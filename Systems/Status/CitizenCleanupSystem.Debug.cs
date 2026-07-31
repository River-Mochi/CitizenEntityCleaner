// File: CitizenCleanupSystem.Debug.cs
namespace CitizenCleaner
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Game.Simulation;
    using Unity.Collections;
    using Unity.Entities;

    public partial class CitizenCleanupSystem
    {
        // Corrupt gets extra IDs; every other report sample stays short.
        private const int kCorruptSampleLimit = 25;
        private const int kReportSampleLimit = 10;

        public readonly struct CitizenCountSnapshot
        {
            public readonly int CCHouseholdMemberEntities;
            public readonly int GameValidMovedInCitizens;
            public readonly int GameHomelessCitizens;
            public readonly int GameMovingInHouseholds;
            public readonly int GameMovingAwayHouseholds;
            public readonly int GameCommuterHouseholds;
            public readonly int GameTouristCitizens;
            public readonly bool GameCountsReady;

            public CitizenCountSnapshot(
                int ccHouseholdMemberEntities,
                int gameValidMovedInCitizens,
                int gameHomelessCitizens,
                int gameMovingInHouseholds,
                int gameMovingAwayHouseholds,
                int gameCommuterHouseholds,
                int gameTouristCitizens,
                bool gameCountsReady)
            {
                CCHouseholdMemberEntities = ccHouseholdMemberEntities;
                GameValidMovedInCitizens = gameValidMovedInCitizens;
                GameHomelessCitizens = gameHomelessCitizens;
                GameMovingInHouseholds = gameMovingInHouseholds;
                GameMovingAwayHouseholds = gameMovingAwayHouseholds;
                GameCommuterHouseholds = gameCommuterHouseholds;
                GameTouristCitizens = gameTouristCitizens;
                GameCountsReady = gameCountsReady;
            }
        }

        private struct DiagnosticCitizenCounts
        {
            public int Corrupt;
            public int MovingAway;
            public int Commuters;
            public int Homeless;
            public int HomelessHouseholdMembers;
            public int HomelessMissingCitizen;
            public int HomelessTourist;
            public int HomelessCommuter;
            public int HomelessMissingValidCitizen;
            public int HomelessMissingValidMovingAway;
            public int HomelessMissingValidNotMovedIn;
            public int HomelessMissingValidMovedInMismatch;
            public int HomelessDead;
            public int HomelessMissingFlag;
            public int NormalPropertySeekerHouseholds;
            public int HomelessPropertySeekerHouseholds;
            public int NoPropertyRenterNotMovedInHouseholds;
            public int NoPropertyRenterPropertySeekerHouseholds;
        }

        // Game 1.6 mixes citizen + household totals, so keep each explicit.
        public CitizenCountSnapshot GetCitizenCountSnapshot()
        {
            int ccHouseholdMemberEntities =
                m_householdMemberQuery.CalculateEntityCount();
            CountHouseholdDataSystem? gameCounts =
                World.GetExistingSystemManaged<CountHouseholdDataSystem>();

            if (gameCounts == null)
            {
                return new CitizenCountSnapshot(
                    ccHouseholdMemberEntities,
                    0, 0, 0, 0, 0, 0,
                    gameCountsReady: false);
            }

            CountHouseholdDataSystem.HouseholdData data =
                gameCounts.GetHouseholdCountData();

            return new CitizenCountSnapshot(
                ccHouseholdMemberEntities,
                data.m_MovedInCitizenCount,
                data.m_HomelessCitizenCount,
                data.m_MovingInHouseholdCount,
                data.m_MovingAwayHouseholdCount,
                data.m_CommuterHouseholdCount,
                data.m_TouristCitizenCount,
                !gameCounts.IsCountDataNotReady());
        }

        public void LogDiagnosticReportToLog()
        {
            if (!HasAnyCitizenData())
            {
                s_Log.Info("[Report] No city loaded. Load a city first.");
                return;
            }

            try
            {
                WriteDiagnosticReport();
            }
            catch (Exception ex)
            {
                s_Log.Error(
                    $"[Report] Diagnostic report failed: " +
                    $"{ex.GetType().Name}: {ex.Message}");
#if DEBUG
                s_Log.Debug(ex.ToString());
#endif
            }
        }

        private void WriteDiagnosticReport()
        {
            using NativeList<Entity> corrupt =
                new NativeList<Entity>(kCorruptSampleLimit, Allocator.Temp);
            using NativeList<Entity> movingAway =
                new NativeList<Entity>(kReportSampleLimit, Allocator.Temp);
            using NativeList<Entity> commuters =
                new NativeList<Entity>(kReportSampleLimit, Allocator.Temp);
            using NativeList<Entity> homeless =
                new NativeList<Entity>(kReportSampleLimit, Allocator.Temp);
            using NativeList<Entity> notMovedInWithoutProperty =
                new NativeList<Entity>(kReportSampleLimit, Allocator.Temp);
            using NativeList<Entity> propertySeekersWithoutProperty =
                new NativeList<Entity>(kReportSampleLimit, Allocator.Temp);
            List<string> homelessMismatchDetails =
                new List<string>(kReportSampleLimit);

            DiagnosticCitizenCounts diagnosticCounts =
                CollectDiagnosticCitizenSamples(
                    corrupt,
                    movingAway,
                    commuters,
                    homeless,
                    notMovedInWithoutProperty,
                    propertySeekersWithoutProperty,
                    homelessMismatchDetails);

            CitizenCountSnapshot gameCounts = GetCitizenCountSnapshot();
            CitizenVehicleStatusSystem.Snapshot? vehicleSnapshot = null;

            try
            {
                if (Mod.VehicleStatusSystem != null)
                {
                    vehicleSnapshot =
                        Mod.VehicleStatusSystem.BuildSnapshot(
                            collectSamples: true);
                }
            }
            catch (Exception ex)
            {
                s_Log.Warn(
                    $"[Report] Vehicle snapshot unavailable: " +
                    $"{ex.GetType().Name}: {ex.Message}");
            }

            StringBuilder report = new StringBuilder(8192);
            report.AppendLine();
            report.AppendLine("============================================================");
            report.AppendLine(
                string.Format(
                    ReportText(
                        "Header",
                        "CITIZEN CLEANER — LOG REPORT\nGenerated: {0}"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            report.AppendLine("============================================================");
            report.AppendLine();

            AppendCitizenCrossCheck(report, diagnosticCounts, gameCounts);
            AppendHouseholdHousingDiagnostics(
                report,
                diagnosticCounts,
                gameCounts,
                notMovedInWithoutProperty,
                propertySeekersWithoutProperty);
            AppendHomelessDiagnostics(
                report,
                diagnosticCounts,
                homelessMismatchDetails);

            report.AppendLine(ReportText(
                "CitizenIdsHeading",
                "[CITIZEN ENTITY IDs — use Scene Explorer; Index:Version]"));
            AppendEntitySection(
                report,
                ReportText("CorruptCitizens", "Corrupt citizens"),
                diagnosticCounts.Corrupt,
                corrupt);
            AppendEntitySection(
                report,
                ReportText(
                    "MovingAwayCitizens",
                    "Moving-away citizens"),
                diagnosticCounts.MovingAway,
                movingAway);
            AppendEntitySection(
                report,
                ReportText("CommuterCitizens", "Commuter citizens"),
                diagnosticCounts.Commuters,
                commuters);
            AppendEntitySection(
                report,
                ReportText("HomelessCitizens", "Eligible homeless citizens"),
                diagnosticCounts.Homeless,
                homeless);

            if (vehicleSnapshot.HasValue)
                AppendVehicleReport(report, vehicleSnapshot.Value);
            else
                report.AppendLine(
                    ReportText(
                        "VehicleSnapshotUnavailable",
                        "[PERSONAL VEHICLES]\nVehicle snapshot unavailable.\n"));

            report.AppendLine("============================================================");
            s_Log.Info(report.ToString());
        }

        private static string ReportText(string key, string fallback)
        {
            return CCSetting.L("CitizenCleaner/Report/" + key, fallback);
        }
    }
}
