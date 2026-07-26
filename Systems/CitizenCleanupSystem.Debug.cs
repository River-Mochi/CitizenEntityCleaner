// CitizenCleanupSystem.Debug.cs
using System;
using System.Text;
using Game.Simulation;
using Unity.Collections;
using Unity.Entities;

namespace CitizenCleaner
{
    // PART: Debug — one read-only, player-readable report.
    public partial class CitizenCleanupSystem : SystemBase
    {
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

        public CitizenCountSnapshot GetCitizenCountSnapshot()
        {
            int ccHouseholdMemberEntities = m_householdMemberQuery.CalculateEntityCount();
            CountHouseholdDataSystem gameCounts =
                World.GetOrCreateSystemManaged<CountHouseholdDataSystem>();
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
                s_Log.Info("[Report] No city loaded (no citizen data). Load a city first.");
                return;
            }

            using NativeList<Entity> corrupt = GetDeletionCandidates(
                Allocator.TempJob,
                tally: false,
                overrideWantCorrupt: true,
                overrideWantHomeless: false,
                overrideWantCommuters: false,
                overrideWantMovingAwayNoPR: false);

            using NativeList<Entity> movingAway = GetDeletionCandidates(
                Allocator.TempJob,
                tally: false,
                overrideWantCorrupt: false,
                overrideWantHomeless: false,
                overrideWantCommuters: false,
                overrideWantMovingAwayNoPR: true);

            using NativeList<Entity> commuters = GetDeletionCandidates(
                Allocator.TempJob,
                tally: false,
                overrideWantCorrupt: false,
                overrideWantHomeless: false,
                overrideWantCommuters: true,
                overrideWantMovingAwayNoPR: false);

            using NativeList<Entity> homeless = GetDeletionCandidates(
                Allocator.TempJob,
                tally: false,
                overrideWantCorrupt: false,
                overrideWantHomeless: true,
                overrideWantCommuters: false,
                overrideWantMovingAwayNoPR: false);

            CitizenCountSnapshot citizenCounts = GetCitizenCountSnapshot();
            CitizenVehicleStatusSystem.Snapshot? vehicleSnapshot = null;

            try
            {
                if (Mod.VehicleStatusSystem != null)
                    vehicleSnapshot = Mod.VehicleStatusSystem.BuildSnapshot();
            }
            catch (Exception ex)
            {
                s_Log.Warn($"[Report] Vehicle snapshot failed: {ex.GetType().Name}: {ex.Message}");
            }

            var sb = new StringBuilder(4096);
            sb.AppendLine();
            sb.AppendLine("======================================================================");
            sb.AppendLine("CITIZEN CLEANER — READ-ONLY DIAGNOSTIC REPORT");
            sb.AppendLine("======================================================================");
            sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine("This report does not delete or modify entities.");
            sb.AppendLine();

            sb.AppendLine("[CITIZEN COUNTS]");
            sb.AppendLine($"CC household-member entities : {citizenCounts.CCHouseholdMemberEntities:N0}");
            sb.AppendLine($"Game valid moved-in citizens  : {citizenCounts.GameValidMovedInCitizens:N0}");
            sb.AppendLine(
                $"Difference (CC - game)         : " +
                $"{citizenCounts.CCHouseholdMemberEntities - citizenCounts.GameValidMovedInCitizens:+#,0;-#,0;0}");
            sb.AppendLine($"Game homeless citizens        : {citizenCounts.GameHomelessCitizens:N0}");
            sb.AppendLine($"Game moving-away households   : {citizenCounts.GameMovingAwayHouseholds:N0}");
            sb.AppendLine($"Game commuter households      : {citizenCounts.GameCommuterHouseholds:N0}");
            sb.AppendLine($"Game tourist citizens         : {citizenCounts.GameTouristCitizens:N0}");
            sb.AppendLine($"Game count data ready          : {(citizenCounts.GameCountsReady ? "yes" : "no; values may be cached/initializing")}");
            sb.AppendLine(
                "Note: the two totals intentionally have different meanings. CC counts every non-deleted " +
                "HouseholdMember entity; the game count includes only valid moved-in citizens.");
            sb.AppendLine();

            sb.AppendLine("[CITIZEN ENTITY SAMPLES — Index:Version]");
            AppendEntitySection(sb, "Corrupt citizens", corrupt, 25);
            AppendEntitySection(sb, "Moving-away citizens (household MovingAway + no PropertyRenter)", movingAway, 10);
            AppendEntitySection(sb, "Commuter citizens", commuters, 10);
            AppendEntitySection(sb, "Homeless citizens", homeless, 10);

            if (vehicleSnapshot.HasValue)
            {
                CitizenVehicleStatusSystem.Snapshot vehicles = vehicleSnapshot.Value;

                sb.AppendLine("[PERSONAL CARS — bicycles excluded]");
                sb.AppendLine(
                    $"Total {vehicles.CarTotal:N0} | active {vehicles.CarActive:N0} | " +
                    $"parked {vehicles.CarParked:N0} | transitioning/other {vehicles.CarTransitioning:N0}");
                sb.AppendLine(
                    $"Parked: street {vehicles.CarParkedOnStreet:N0} | " +
                    $"building/parking facility {vehicles.CarParkedAtFacility:N0} " +
                    $"(hidden {vehicles.CarHiddenAtFacility:N0}) | " +
                    $"OC hidden {vehicles.CarHiddenAtOutsideConnection:N0} | " +
                    $"other {vehicles.CarParkedOther:N0} (hidden {vehicles.CarHiddenOther:N0})");
                sb.AppendLine($"Ownership mismatches: {vehicles.CarOwnershipMismatch:N0}");
                sb.AppendLine(
                    $"OC-hidden owner location: city household {vehicles.CarOcHiddenCityHouseholdOwner:N0} | " +
                    $"at OC {vehicles.CarOcHiddenOwnerAtOutsideConnection:N0} | " +
                    $"nonresident/moving {vehicles.CarOcHiddenNonResidentOrMovingOwner:N0} | " +
                    $"missing/non-household {vehicles.CarOcHiddenMissingOrNonHouseholdOwner:N0}");
                sb.AppendLine(
                    $"OC-hidden cars with broken ownership backlink: " +
                    $"{vehicles.CarOcHiddenOwnershipMismatch:N0}");
                sb.AppendLine(
                    $"OC-hidden staging evidence: parked lane at OC " +
                    $"{vehicles.CarOcHiddenLaneAtOutsideConnection:N0} | " +
                    $"TripSource at OC {vehicles.CarOcHiddenTripSourceAtOutsideConnection:N0} | " +
                    $"TripSource at OC with no parked lane {vehicles.CarOcHiddenTripSourceWithoutLane:N0}");
                sb.AppendLine(
                    $"OC-hidden trip state: HomeTarget {vehicles.CarOcHiddenHomeTarget:N0} | " +
                    $"keeper currently at OC {vehicles.CarOcHiddenKeeperAtOutsideConnection:N0}");
                sb.AppendLine();

                sb.AppendLine("[BICYCLES]");
                sb.AppendLine(
                    $"Total {vehicles.BicycleTotal:N0} | active {vehicles.BicycleActive:N0} | " +
                    $"parked {vehicles.BicycleParked:N0} | transitioning/other {vehicles.BicycleTransitioning:N0}");
                sb.AppendLine(
                    $"Parked: visible {vehicles.BicycleVisibleParked:N0} | " +
                    $"OC hidden {vehicles.BicycleHiddenAtOutsideConnection:N0} | " +
                    $"hidden elsewhere {vehicles.BicycleHiddenOther:N0}");
                sb.AppendLine($"Keeper mismatches: {vehicles.BicycleOwnershipMismatch:N0}");
                sb.AppendLine();

                sb.AppendLine("[VEHICLE DEFINITIONS]");
                sb.AppendLine("Street = ParkedCar + visible (not Unspawned) + ParkingLane, excluding facility-owned lanes.");
                sb.AppendLine("Building/parking facility = GarageLane, ParkingFacility, CarParkingFacility, or a Building in the lane owner chain.");
                sb.AppendLine(
                    "OC hidden = ParkedCar + Unspawned + either its parked-lane owner chain or " +
                    "TripSource reaches an outside connection.");
                sb.AppendLine(
                    "TripSource at OC with no parked lane matches the game's initial no-nearby-parking " +
                    "fallback; it is not proof that the vehicle is abandoned.");
                sb.AppendLine("Ownership mismatch mirrors the game's PersonalCarOwnerSystem backlink validation.");
                sb.AppendLine("Owner-at-OC is diagnostic only: it can be a legitimate commuter or moving-away transition.");
                sb.AppendLine("The game has no Abandoned component or parked-since timestamp for personal cars.");
                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("[PERSONAL VEHICLES]");
                sb.AppendLine("Vehicle snapshot unavailable; see the warning immediately before this report.");
                sb.AppendLine();
            }

            sb.AppendLine("======================================================================");
            s_Log.Info(sb.ToString());
        }

        private static void AppendEntitySection(
            StringBuilder sb,
            string title,
            NativeList<Entity> entities,
            int sampleLimit)
        {
            int sampleCount = Math.Min(sampleLimit, entities.Length);
            sb.AppendLine($"{title}: total {entities.Length:N0}; showing {sampleCount:N0}");
            sb.Append("IDs: ");

            if (sampleCount == 0)
            {
                sb.AppendLine("(none)");
                sb.AppendLine();
                return;
            }

            for (var i = 0; i < sampleCount; i++)
            {
                if (i > 0)
                    sb.Append(", ");

                sb.Append(FormatIndexVersion(entities[i]));
            }

            sb.AppendLine();
            sb.AppendLine();
        }
    }
}
