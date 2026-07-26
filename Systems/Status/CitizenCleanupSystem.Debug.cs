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
            report.AppendLine("======================================================================");
            report.AppendLine("CITIZEN CLEANER — READ-ONLY DIAGNOSTIC REPORT");
            report.AppendLine("======================================================================");
            report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            report.AppendLine("This report does not delete or modify entities.");
            report.AppendLine();

            report.AppendLine("[CITIZEN COUNTS]");
            report.AppendLine(
                $"CC household-member entities : " +
                $"{citizenCounts.CCHouseholdMemberEntities:N0}");
            report.AppendLine(
                $"Game valid moved-in citizens  : " +
                $"{citizenCounts.GameValidMovedInCitizens:N0}");
            report.AppendLine(
                $"Difference (CC - game)         : " +
                $"{citizenCounts.CCHouseholdMemberEntities - citizenCounts.GameValidMovedInCitizens:+#,0;-#,0;0}");
            report.AppendLine(
                $"Game homeless citizens        : " +
                $"{citizenCounts.GameHomelessCitizens:N0}");
            report.AppendLine(
                $"Game moving-away households   : " +
                $"{citizenCounts.GameMovingAwayHouseholds:N0}");
            report.AppendLine(
                $"Game commuter households      : " +
                $"{citizenCounts.GameCommuterHouseholds:N0}");
            report.AppendLine(
                $"Game tourist citizens         : " +
                $"{citizenCounts.GameTouristCitizens:N0}");
            report.AppendLine(
                $"Game count data ready         : " +
                $"{(citizenCounts.GameCountsReady ? "yes" : "no; values may still be initializing")}");
            report.AppendLine(
                "Note: CC counts every non-deleted HouseholdMember entity. " +
                "The game total includes only valid moved-in citizens.");
            report.AppendLine();

            report.AppendLine("[CITIZEN ENTITY SAMPLES — Index:Version]");
            AppendEntitySection(
                report,
                "Corrupt citizens",
                diagnosticCounts.Corrupt,
                corrupt);
            AppendEntitySection(
                report,
                "Moving-away citizens (household MovingAway + no PropertyRenter)",
                diagnosticCounts.MovingAway,
                movingAway);
            AppendEntitySection(
                report,
                "Commuter citizens",
                diagnosticCounts.Commuters,
                commuters);
            AppendEntitySection(
                report,
                "Homeless citizens",
                diagnosticCounts.Homeless,
                homeless);

            if (vehicleSnapshot.HasValue)
                AppendVehicleReport(report, vehicleSnapshot.Value);
            else
                report.AppendLine("[PERSONAL VEHICLES]\nVehicle snapshot unavailable.\n");

            report.AppendLine("======================================================================");
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
                $"{title}: total {total:N0}; showing {samples.Length:N0}");
            report.Append("IDs: ");

            if (samples.Length == 0)
            {
                report.AppendLine("(none)");
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
            report.AppendLine("[PERSONAL CARS — bicycles excluded]");
            report.AppendLine(
                $"Total {vehicles.CarTotal:N0} | active {vehicles.CarActive:N0} | " +
                $"parked {vehicles.CarParked:N0} | other {vehicles.CarTransitioning:N0}");
            report.AppendLine(
                $"Parked: street {vehicles.CarParkedOnStreet:N0} | " +
                $"building/parking facility {vehicles.CarParkedAtFacility:N0} " +
                $"(hidden {vehicles.CarHiddenAtFacility:N0}) | " +
                $"OC hidden {vehicles.CarHiddenAtOutsideConnection:N0} | " +
                $"other {vehicles.CarParkedOther:N0} (hidden {vehicles.CarHiddenOther:N0})");
            report.AppendLine();

            report.AppendLine("[POTENTIAL CAR ORPHANS — point-in-time]");
            report.AppendLine(
                $"Total {vehicles.CarOwnershipMismatch:N0} | " +
                $"missing Owner {vehicles.CarMissingOwner:N0} | " +
                $"owner missing OwnedVehicle buffer {vehicles.CarOwnerMissingBuffer:N0} | " +
                $"backlink missing {vehicles.CarOwnerMissingBacklink:N0}");
            report.AppendLine(
                $"Parked mismatch location: street {vehicles.CarStreetOwnershipMismatch:N0} | " +
                $"facility {vehicles.CarFacilityOwnershipMismatch:N0} | " +
                $"OC hidden {vehicles.CarOcHiddenOwnershipMismatch:N0} | " +
                $"other {vehicles.CarOtherParkedOwnershipMismatch:N0}");
            report.AppendLine();

            report.AppendLine("[OC-HIDDEN CARS]");
            report.AppendLine(
                $"Owner: city household {vehicles.CarOcHiddenCityHouseholdOwner:N0} | " +
                $"household at OC {vehicles.CarOcHiddenHouseholdAtOutsideConnection:N0} | " +
                $"direct OC entity {vehicles.CarOcHiddenDirectOutsideConnectionOwner:N0} | " +
                $"nonresident/moving {vehicles.CarOcHiddenNonResidentOrMovingOwner:N0} | " +
                $"missing/non-household {vehicles.CarOcHiddenMissingOrNonHouseholdOwner:N0}");
            report.AppendLine(
                $"Staging evidence: parked lane at OC " +
                $"{vehicles.CarOcHiddenLaneAtOutsideConnection:N0} | " +
                $"TripSource at OC {vehicles.CarOcHiddenTripSourceAtOutsideConnection:N0} | " +
                $"TripSource at OC with no lane {vehicles.CarOcHiddenTripSourceWithoutLane:N0}");
            report.AppendLine(
                $"Trip state: HomeTarget {vehicles.CarOcHiddenHomeTarget:N0} | " +
                $"keeper at OC {vehicles.CarOcHiddenKeeperAtOutsideConnection:N0}");
            report.AppendLine();

            report.AppendLine("[BICYCLES]");
            report.AppendLine(
                $"Total {vehicles.BicycleTotal:N0} | active {vehicles.BicycleActive:N0} | " +
                $"parked {vehicles.BicycleParked:N0} | other {vehicles.BicycleTransitioning:N0}");
            report.AppendLine(
                $"Parked: visible {vehicles.BicycleVisibleParked:N0} | " +
                $"OC hidden {vehicles.BicycleHiddenAtOutsideConnection:N0} | " +
                $"hidden elsewhere {vehicles.BicycleHiddenOther:N0}");
            report.AppendLine(
                $"Keeper mismatches: {vehicles.BicycleOwnershipMismatch:N0}");
            report.AppendLine();

            report.AppendLine("[VEHICLE DEFINITIONS]");
            report.AppendLine(
                "Street = visible ParkedCar on a ParkingLane, excluding facility lanes.");
            report.AppendLine(
                "Facility = GarageLane, ParkingFacility, CarParkingFacility, " +
                "or Building in the parked-lane owner chain.");
            report.AppendLine(
                "OC hidden = ParkedCar + Unspawned whose parked lane or TripSource " +
                "reaches an outside connection.");
            report.AppendLine(
                "Ownership mismatches mirror PersonalCarOwnerSystem and can be temporary.");
            report.AppendLine(
                "No location or ownership count alone proves that a vehicle is abandoned.");
            report.AppendLine();
        }
    }
}
