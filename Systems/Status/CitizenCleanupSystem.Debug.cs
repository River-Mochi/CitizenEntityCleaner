namespace CitizenCleaner
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Game.Citizens;
    using Game.Simulation;
    using Unity.Collections;
    using Unity.Entities;

    public partial class CitizenCleanupSystem
    {
        private const int kCorruptSampleLimit = 25;
        private const int kOtherSampleLimit = 10;
        private const int kMismatchSampleLimit = 15;

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
            public int HomelessHouseholdMembers;
            public int HomelessMissingCitizen;
            public int HomelessTourist;
            public int HomelessCommuter;
            public int HomelessInvalidCitizen;
            public int HomelessDead;
            public int HomelessMissingFlag;
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
            List<string> homelessMismatchIds =
                new List<string>(kMismatchSampleLimit);

            DiagnosticCitizenCounts diagnosticCounts =
                CollectDiagnosticCitizenSamples(
                    corrupt,
                    movingAway,
                    commuters,
                    homeless,
                    homelessMismatchIds);

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
            AppendHomelessDiagnostics(
                report,
                diagnosticCounts,
                homelessMismatchIds);

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

        private static void AppendCitizenCrossCheck(
            StringBuilder report,
            DiagnosticCitizenCounts cc,
            CitizenCountSnapshot game)
        {
            report.AppendLine(ReportText(
                "CitizenCrossCheckHeading",
                "[CITIZEN COUNT CROSS-CHECK — GAME 1.6]"));
            report.AppendLine(
                $"CC total: {game.CCHouseholdMemberEntities:N0} " +
                "household-member entities");
            report.AppendLine(
                $"Game total: {game.GameValidMovedInCitizens:N0} " +
                "valid moved-in citizens");
            report.AppendLine();
            report.AppendLine(
                $"Homeless: CC {cc.Homeless:N0} citizens | " +
                $"game {game.GameHomelessCitizens:N0} valid citizens");
            report.AppendLine(
                $"Moving-away: CC {cc.MovingAway:N0} citizens | " +
                $"game {game.GameMovingAwayHouseholds:N0} households");
            report.AppendLine(
                $"Commuters: CC {cc.Commuters:N0} citizens | " +
                $"game {game.GameCommuterHouseholds:N0} households");
            report.AppendLine(
                $"Tourists: game {game.GameTouristCitizens:N0} citizens");
            report.AppendLine(ReportText(
                "CitizenCrossCheckNote",
                "Game 1.6 counters use game population rules. " +
                "CC counts cleanup candidates; moving-away and commuter " +
                "game values count households, not citizens."));

            if (!game.GameCountsReady)
            {
                report.AppendLine(ReportText(
                    "GameCountsPending",
                    "Game counts are still initializing."));
            }

            report.AppendLine();
        }

        private static void AppendHomelessDiagnostics(
            StringBuilder report,
            DiagnosticCitizenCounts counts,
            List<string> mismatchIds)
        {
            int excluded =
                counts.HomelessHouseholdMembers - counts.Homeless;

            report.AppendLine(ReportText(
                "HomelessCheckHeading",
                "[HOMELESS ELIGIBILITY CHECK]"));
            report.AppendLine(
                $"{counts.HomelessHouseholdMembers:N0} members of " +
                $"HomelessHousehold | {counts.Homeless:N0} eligible | " +
                $"{excluded:N0} excluded");
            report.AppendLine(
                $"Excluded: {counts.HomelessDead:N0} dead | " +
                $"{counts.HomelessInvalidCitizen:N0} invalid | " +
                $"{counts.HomelessCommuter:N0} commuter | " +
                $"{counts.HomelessTourist:N0} tourist | " +
                $"{counts.HomelessMissingFlag:N0} missing Homeless flag | " +
                $"{counts.HomelessMissingCitizen:N0} missing Citizen");
            report.AppendLine(
                "Excluded IDs: " +
                (mismatchIds.Count == 0
                    ? ReportText("None", "(none)")
                    : string.Join(", ", mismatchIds)));
            report.AppendLine();
        }

        private DiagnosticCitizenCounts CollectDiagnosticCitizenSamples(
            NativeList<Entity> corrupt,
            NativeList<Entity> movingAway,
            NativeList<Entity> commuters,
            NativeList<Entity> homeless,
            List<string> homelessMismatchIds)
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

                    if (type == CleanupType.Homeless)
                    {
                        counts.HomelessHouseholdMembers++;
                        HomelessExclusionReason reason =
                            GetHomelessExclusionReason(citizen);

                        if (reason == HomelessExclusionReason.None)
                        {
                            counts.Homeless++;
                            AddSample(
                                homeless,
                                citizen,
                                kOtherSampleLimit);
                        }
                        else
                        {
                            CountHomelessExclusion(ref counts, reason);
                            if (homelessMismatchIds.Count <
                                kMismatchSampleLimit)
                            {
                                homelessMismatchIds.Add(
                                    $"{FormatIndexVersion(citizen)} ({reason})");
                            }
                        }

                        continue;
                    }

                    switch (type)
                    {
                        case CleanupType.Corrupt:
                            counts.Corrupt++;
                            AddSample(
                                corrupt,
                                citizen,
                                kCorruptSampleLimit);
                            break;
                        case CleanupType.MovingAway:
                            counts.MovingAway++;
                            AddSample(
                                movingAway,
                                citizen,
                                kOtherSampleLimit);
                            break;
                        case CleanupType.Commuters:
                            counts.Commuters++;
                            AddSample(
                                commuters,
                                citizen,
                                kOtherSampleLimit);
                            break;
                    }
                }
            }

            return counts;
        }

        private static void CountHomelessExclusion(
            ref DiagnosticCitizenCounts counts,
            HomelessExclusionReason reason)
        {
            switch (reason)
            {
                case HomelessExclusionReason.MissingCitizen:
                    counts.HomelessMissingCitizen++;
                    break;
                case HomelessExclusionReason.Tourist:
                    counts.HomelessTourist++;
                    break;
                case HomelessExclusionReason.Commuter:
                    counts.HomelessCommuter++;
                    break;
                case HomelessExclusionReason.InvalidCitizen:
                    counts.HomelessInvalidCitizen++;
                    break;
                case HomelessExclusionReason.Dead:
                    counts.HomelessDead++;
                    break;
                case HomelessExclusionReason.MissingHomelessFlag:
                    counts.HomelessMissingFlag++;
                    break;
            }
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
                $"{title}: {total:N0} total | {samples.Length:N0} IDs");
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
            report.AppendLine("[PERSONAL VEHICLES]");
            report.AppendLine();
            report.AppendLine("[PERSONAL CARS]");
            report.AppendLine(
                $"{vehicles.CarActive:N0} active | " +
                $"{vehicles.CarParked:N0} parked | " +
                $"{vehicles.CarTransitioning:N0} transitioning | " +
                $"{vehicles.CarTotal:N0} total");
            report.AppendLine(
                $"{vehicles.CarDummyTraffic:N0} DummyTraffic");
            report.AppendLine();

            report.AppendLine("[PARKED LOCATIONS]");
            report.AppendLine(
                $"{vehicles.CarParkedOnStreet:N0} street | " +
                $"{vehicles.CarParkedAtFacility:N0} facility " +
                $"({vehicles.CarHiddenAtFacility:N0} hidden) | " +
                $"{vehicles.CarHiddenAtOutsideConnection:N0} at OC | " +
                $"{vehicles.CarParkedOther:N0} other");
            report.AppendLine(
                $"Other: {vehicles.CarOtherLaneNull:N0} null lane | " +
                $"{vehicles.CarOtherHiddenParkingLane:N0} hidden ParkingLane | " +
                $"{vehicles.CarOtherHiddenNonParkingLane:N0} hidden other lane | " +
                $"{vehicles.CarOtherVisibleNonParkingLane:N0} visible other lane");
            report.AppendLine();

            report.AppendLine("[CARS AT OUTSIDE CONNECTION]");
            report.AppendLine(
                $"Owners: {vehicles.CarOcHiddenCityHouseholdOwner:N0} city | " +
                $"{vehicles.CarOcHiddenHouseholdAtOutsideConnection:N0} " +
                "household at OC | " +
                $"{vehicles.CarOcHiddenDirectOutsideConnectionOwner:N0} " +
                "direct OC owner | " +
                $"{vehicles.CarOcHiddenNonResidentOrMovingOwner:N0} away | " +
                $"{vehicles.CarOcHiddenMissingOrNonHouseholdOwner:N0} missing");
            report.AppendLine(
                $"DummyTraffic: {vehicles.CarOcHiddenDummyTraffic:N0} total | " +
                $"{vehicles.CarOcHiddenDirectOwnerDummyTraffic:N0} " +
                "with direct OC owner");
            report.AppendLine(
                $"Location evidence: " +
                $"{vehicles.CarOcHiddenLaneAtOutsideConnection:N0} lane at OC | " +
                $"{vehicles.CarOcHiddenTripSourceAtOutsideConnection:N0} " +
                "TripSource at OC | " +
                $"{vehicles.CarOcHiddenTripSourceWithoutLane:N0} " +
                "TripSource at OC with null lane");
            report.AppendLine(
                $"Trip state: {vehicles.CarOcHiddenHomeTarget:N0} HomeTarget | " +
                $"{vehicles.CarOcHiddenKeeperAtOutsideConnection:N0} keeper at OC");
            report.AppendLine();

            report.AppendLine("[POSSIBLE ORPHANS]");
            report.AppendLine(
                $"{vehicles.CarOwnershipMismatch:N0} total | " +
                $"{vehicles.CarMissingOwner:N0} missing Owner | " +
                $"{vehicles.CarOwnerMissingBuffer:N0} owner missing buffer | " +
                $"{vehicles.CarOwnerMissingBacklink:N0} backlink missing");
            report.AppendLine(
                "The game validates personal-vehicle ownership every 1,024 " +
                "simulation ticks, processing one of 16 update groups each run. " +
                "Zero or a small temporary count is expected.");
            report.AppendLine();

            report.AppendLine("[BICYCLE GROUP]");
            report.AppendLine(
                $"{vehicles.BicycleActive:N0} active | " +
                $"{vehicles.BicycleParked:N0} parked | " +
                $"{vehicles.BicycleTransitioning:N0} transitioning | " +
                $"{vehicles.BicycleTotal:N0} total");
            report.AppendLine(
                $"{vehicles.BicycleVisibleParked:N0} visible parked | " +
                $"{vehicles.BicycleHiddenAtOutsideConnection:N0} at OC | " +
                $"{vehicles.BicycleHiddenOther:N0} hidden elsewhere | " +
                $"{vehicles.BicycleOwnershipMismatch:N0} keeper mismatches");
            report.AppendLine();

            report.AppendLine("[TRAILERS]");
            report.AppendLine(
                $"{vehicles.TrailerTotal:N0} total | " +
                $"{vehicles.TrailerHidden:N0} hidden | " +
                $"{vehicles.TrailerMissingController:N0} missing controller");
            report.AppendLine();

            report.AppendLine(
                "[VEHICLE ENTITY IDs — use Scene Explorer; Index:Version]");
            AppendEntityArray(
                report,
                "Cars at OC",
                vehicles.CarOcHiddenSamples);
            AppendEntityArray(
                report,
                "Direct OC owner",
                vehicles.CarOcHiddenDirectOwnerSamples);
            AppendEntityArray(
                report,
                "Away owner at OC",
                vehicles.CarOcHiddenNonResidentSamples);
            AppendEntityArray(
                report,
                "Missing/non-household owner at OC",
                vehicles.CarOcHiddenMissingOwnerSamples);
            AppendEntityArray(
                report,
                "Parked Other",
                vehicles.CarParkedOtherSamples);
            AppendEntityArray(
                report,
                "Parked with null lane",
                vehicles.CarParkedLaneNullSamples);
            AppendEntityArray(
                report,
                "Ownership mismatch",
                vehicles.CarOwnershipMismatchSamples);
            AppendEntityArray(
                report,
                "Trailer missing controller",
                vehicles.TrailerMissingControllerSamples);
            report.AppendLine();

            report.AppendLine("[LOCATION DEFINITIONS]");
            report.AppendLine(
                "Street = visible ParkedCar on a ParkingLane, excluding facilities.");
            report.AppendLine(
                "Facility = lane owner chain reaches a building, garage, or parking facility.");
            report.AppendLine(
                "At OC = hidden ParkedCar whose lane or TripSource reaches an Outside Connection.");
            report.AppendLine(
                "Other = parked car not matched above; subcounts and IDs show why.");
            report.AppendLine(
                "Direct OC owners marked DummyTraffic are game-created traffic, not resident-owned cars.");
            report.AppendLine();
        }

        private static void AppendEntityArray(
            StringBuilder report,
            string title,
            Entity[]? samples)
        {
            report.Append(title);
            report.Append(": ");

            if (samples == null || samples.Length == 0)
            {
                report.AppendLine(ReportText("None", "(none)"));
                return;
            }

            for (int i = 0; i < samples.Length; i++)
            {
                if (i > 0)
                    report.Append(", ");

                report.Append(FormatIndexVersion(samples[i]));
            }

            report.AppendLine();
        }

        private static string ReportText(string key, string fallback)
        {
            return CCSetting.L("CitizenCleaner/Report/" + key, fallback);
        }
    }
}
