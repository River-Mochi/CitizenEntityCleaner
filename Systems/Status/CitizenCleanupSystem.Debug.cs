// CitizenCleanupSystem.Debug.cs
using System;
using System.Text;
using Game.Citizens;
using Game.Simulation;
using Unity.Collections;
using Unity.Entities;

namespace CitizenCleaner
{
    public partial class CitizenCleanupSystem
    {
        private const int kCorruptSampleLimit = 25;
        private const int kOtherSampleLimit = 10;

        public readonly struct CitizenCountSnapshot
        {
            public readonly int CCHouseholdMemberEntities;
            public readonly int GameValidMovedInCitizens;
            public readonly int GameHomelessCitizens;
            public readonly int GameMovingAwayHouseholds;
            public readonly int GameCommuterHouseholds;
            public readonly int GameTouristCitizens;
            public readonly bool GameCountsReady;

            public CitizenCountSnapshot(
                int ccHouseholdMemberEntities,
                int gameValidMovedInCitizens,
                int gameHomelessCitizens,
                int gameMovingAwayHouseholds,
                int gameCommuterHouseholds,
                int gameTouristCitizens,
                bool gameCountsReady)
            {
                CCHouseholdMemberEntities = ccHouseholdMemberEntities;
                GameValidMovedInCitizens = gameValidMovedInCitizens;
                GameHomelessCitizens = gameHomelessCitizens;
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
        }

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
                    0,
                    0,
                    0,
                    0,
                    0,
                    gameCountsReady: false);
            }

            CountHouseholdDataSystem.HouseholdData data =
                gameCounts.GetHouseholdCountData();

            return new CitizenCountSnapshot(
                ccHouseholdMemberEntities,
                data.m_MovedInCitizenCount,
                data.m_HomelessCitizenCount,
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
                new NativeList<Entity>(kOtherSampleLimit, Allocator.Temp);
            using NativeList<Entity> commuters =
                new NativeList<Entity>(kOtherSampleLimit, Allocator.Temp);
            using NativeList<Entity> homeless =
                new NativeList<Entity>(kOtherSampleLimit, Allocator.Temp);

            DiagnosticCitizenCounts diagnosticCounts =
                CollectDiagnosticCitizenSamples(
                    corrupt,
                    movingAway,
                    commuters,
                    homeless);

            CitizenCountSnapshot citizenCounts = GetCitizenCountSnapshot();
            CitizenVehicleStatusSystem.Snapshot? vehicleSnapshot = null;

            try
            {
                if (Mod.VehicleStatusSystem != null)
                    vehicleSnapshot = Mod.VehicleStatusSystem.BuildSnapshot();
            }
            catch (Exception ex)
            {
                s_Log.Warn(
                    $"[Report] Vehicle snapshot unavailable: " +
                    $"{ex.GetType().Name}: {ex.Message}");
            }

            StringBuilder report = new StringBuilder(4096);
            report.AppendLine();
            report.AppendLine("============================================================");
            report.AppendLine(
                string.Format(
                    ReportText(
                        "Header",
                        "CITIZEN CLEANER — DIAGNOSTIC REPORT\nGenerated: {0}"),
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            report.AppendLine("============================================================");
            report.AppendLine();

            report.AppendLine(
                string.Format(
                    ReportText(
                        "CitizenCounts",
                        "[CITIZEN COUNTS]\n" +
                        "CC household-member entities : {0}\n" +
                        "Game valid moved-in citizens  : {1}\n" +
                        "Difference (CC - game)         : {2}\n" +
                        "Game homeless citizens        : {3}\n" +
                        "Game moving-away households   : {4}\n" +
                        "Game commuter households      : {5}\n" +
                        "Game tourist citizens         : {6}\n" +
                        "CC includes every non-deleted HouseholdMember; " +
                        "the game total includes valid moved-in citizens only."),
                    citizenCounts.CCHouseholdMemberEntities.ToString("N0"),
                    citizenCounts.GameValidMovedInCitizens.ToString("N0"),
                    (citizenCounts.CCHouseholdMemberEntities -
                     citizenCounts.GameValidMovedInCitizens)
                    .ToString("+#,0;-#,0;0"),
                    citizenCounts.GameHomelessCitizens.ToString("N0"),
                    citizenCounts.GameMovingAwayHouseholds.ToString("N0"),
                    citizenCounts.GameCommuterHouseholds.ToString("N0"),
                    citizenCounts.GameTouristCitizens.ToString("N0")));

            if (!citizenCounts.GameCountsReady)
            {
                report.AppendLine(ReportText(
                    "GameCountsPending",
                    "Game counts are still initializing."));
            }

            report.AppendLine();

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
                    "Moving-away citizens (household MovingAway + no PropertyRenter)"),
                diagnosticCounts.MovingAway,
                movingAway);
            AppendEntitySection(
                report,
                ReportText("CommuterCitizens", "Commuter citizens"),
                diagnosticCounts.Commuters,
                commuters);
            AppendEntitySection(
                report,
                ReportText("HomelessCitizens", "Homeless citizens"),
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

        private DiagnosticCitizenCounts CollectDiagnosticCitizenSamples(
            NativeList<Entity> corrupt,
            NativeList<Entity> movingAway,
            NativeList<Entity> commuters,
            NativeList<Entity> homeless)
        {
            DiagnosticCitizenCounts counts = default;
            using NativeArray<Entity> households =
                m_householdQuery.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < households.Length; i++)
            {
                Entity household = households[i];
                CleanupType type = ClassifyHousehold(household);

                if (type == CleanupType.None)
                    continue;

                DynamicBuffer<HouseholdCitizen> members =
                    EntityManager.GetBuffer<HouseholdCitizen>(household);

                for (int j = 0; j < members.Length; j++)
                {
                    Entity citizen = members[j].m_Citizen;
                    if (!IsEligibleCitizen(citizen))
                        continue;

                    switch (type)
                    {
                        case CleanupType.Corrupt:
                            counts.Corrupt++;
                            AddSample(corrupt, citizen, kCorruptSampleLimit);
                            break;
                        case CleanupType.MovingAway:
                            counts.MovingAway++;
                            AddSample(movingAway, citizen, kOtherSampleLimit);
                            break;
                        case CleanupType.Commuters:
                            counts.Commuters++;
                            AddSample(commuters, citizen, kOtherSampleLimit);
                            break;
                        case CleanupType.Homeless:
                            counts.Homeless++;
                            AddSample(homeless, citizen, kOtherSampleLimit);
                            break;
                    }
                }
            }

            return counts;
        }

        private static void AddSample(
            NativeList<Entity> samples,
            Entity entity,
            int limit)
        {
            if (samples.Length < limit)
                samples.Add(entity);
        }

        private static void AppendEntitySection(
            StringBuilder report,
            string title,
            int total,
            NativeList<Entity> samples)
        {
            report.AppendLine(
                string.Format(
                    ReportText(
                        "EntitySampleSummary",
                        "{0}: {1} total | {2} IDs"),
                    title,
                    total.ToString("N0"),
                    samples.Length.ToString("N0")));
            report.Append(ReportText("IdsLabel", "IDs: "));

            if (samples.Length == 0)
            {
                report.AppendLine(ReportText("None", "(none)"));
                report.AppendLine();
                return;
            }

            for (int i = 0; i < samples.Length; i++)
            {
                if (i > 0)
                    report.Append(", ");

                report.Append(FormatIndexVersion(samples[i]));
            }

            report.AppendLine();
            report.AppendLine();
        }

        private static void AppendVehicleReport(
            StringBuilder report,
            CitizenVehicleStatusSystem.Snapshot vehicles)
        {
            report.AppendLine(
                string.Format(
                    ReportText(
                        "PersonalCars",
                        "[PERSONAL CARS — bicycles excluded]\n" +
                        "{0} active | {1} parked | {2} total | {3} other\n" +
                        "Parked: {4} street | {5} facility ({6} hidden) | " +
                        "{7} OC hidden | {8} other ({9} hidden)"),
                    vehicles.CarActive.ToString("N0"),
                    vehicles.CarParked.ToString("N0"),
                    vehicles.CarTotal.ToString("N0"),
                    vehicles.CarTransitioning.ToString("N0"),
                    vehicles.CarParkedOnStreet.ToString("N0"),
                    vehicles.CarParkedAtFacility.ToString("N0"),
                    vehicles.CarHiddenAtFacility.ToString("N0"),
                    vehicles.CarHiddenAtOutsideConnection.ToString("N0"),
                    vehicles.CarParkedOther.ToString("N0"),
                    vehicles.CarHiddenOther.ToString("N0")));
            report.AppendLine();

            report.AppendLine(
                string.Format(
                    ReportText(
                        "PossibleOrphans",
                        "[POSSIBLE ORPHANS]\n" +
                        "{0} total | {1} missing Owner | " +
                        "{2} missing OwnedVehicle buffer | {3} missing backlink\n" +
                        "Parked: {4} street | {5} facility | " +
                        "{6} OC hidden | {7} other"),
                    vehicles.CarOwnershipMismatch.ToString("N0"),
                    vehicles.CarMissingOwner.ToString("N0"),
                    vehicles.CarOwnerMissingBuffer.ToString("N0"),
                    vehicles.CarOwnerMissingBacklink.ToString("N0"),
                    vehicles.CarStreetOwnershipMismatch.ToString("N0"),
                    vehicles.CarFacilityOwnershipMismatch.ToString("N0"),
                    vehicles.CarOcHiddenOwnershipMismatch.ToString("N0"),
                    vehicles.CarOtherParkedOwnershipMismatch.ToString("N0")));
            report.AppendLine();

            report.AppendLine(
                string.Format(
                    ReportText(
                        "OcHiddenCars",
                        "[OC-HIDDEN CARS]\n" +
                        "Owners: {0} city household | {1} household at OC | " +
                        "{2} direct OC entity | {3} nonresident/moving | " +
                        "{4} missing/non-household\n" +
                        "Evidence: {5} parked lane at OC | {6} TripSource at OC | " +
                        "{7} TripSource at OC without lane\n" +
                        "Trip state: {8} HomeTarget | {9} keeper at OC"),
                    vehicles.CarOcHiddenCityHouseholdOwner.ToString("N0"),
                    vehicles.CarOcHiddenHouseholdAtOutsideConnection.ToString("N0"),
                    vehicles.CarOcHiddenDirectOutsideConnectionOwner.ToString("N0"),
                    vehicles.CarOcHiddenNonResidentOrMovingOwner.ToString("N0"),
                    vehicles.CarOcHiddenMissingOrNonHouseholdOwner.ToString("N0"),
                    vehicles.CarOcHiddenLaneAtOutsideConnection.ToString("N0"),
                    vehicles.CarOcHiddenTripSourceAtOutsideConnection.ToString("N0"),
                    vehicles.CarOcHiddenTripSourceWithoutLane.ToString("N0"),
                    vehicles.CarOcHiddenHomeTarget.ToString("N0"),
                    vehicles.CarOcHiddenKeeperAtOutsideConnection.ToString("N0")));
            report.AppendLine();

            report.AppendLine(
                string.Format(
                    ReportText(
                        "Bicycles",
                        "[BICYCLES]\n" +
                        "{0} active | {1} parked | {2} total | {3} other\n" +
                        "Parked: {4} visible | {5} OC hidden | " +
                        "{6} hidden elsewhere\nKeeper mismatches: {7}"),
                    vehicles.BicycleActive.ToString("N0"),
                    vehicles.BicycleParked.ToString("N0"),
                    vehicles.BicycleTotal.ToString("N0"),
                    vehicles.BicycleTransitioning.ToString("N0"),
                    vehicles.BicycleVisibleParked.ToString("N0"),
                    vehicles.BicycleHiddenAtOutsideConnection.ToString("N0"),
                    vehicles.BicycleHiddenOther.ToString("N0"),
                    vehicles.BicycleOwnershipMismatch.ToString("N0")));
            report.AppendLine();

            report.AppendLine(ReportText(
                "Definitions",
                "[DEFINITIONS]\n" +
                "Street: visible ParkedCar on ParkingLane, excluding facilities.\n" +
                "Facility: parked lane owned by a garage, parking facility, or building.\n" +
                "OC hidden: ParkedCar + Unspawned linked to an outside connection.\n" +
                "Ownership mismatches can be temporary; " +
                "location alone does not prove abandonment."));
            report.AppendLine();
        }

        private static string ReportText(string key, string fallback)
        {
            return CCSetting.L("CitizenCleaner/Report/" + key, fallback);
        }
    }
}
