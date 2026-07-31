// File: CitizenCleanupSystem.Debug.Vehicles.cs
namespace CitizenCleaner
{
    using System.Text;
    using Unity.Entities;

    public partial class CitizenCleanupSystem
    {
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
                $"{vehicles.CarDummyTraffic:N0} DummyTraffic personal cars");
            report.AppendLine();

            report.AppendLine("[PARKED LOCATIONS]");
            report.AppendLine(
                $"{vehicles.CarParkedOnStreet:N0} street | " +
                $"{vehicles.CarParkedAtFacility:N0} facility " +
                $"({vehicles.CarHiddenAtFacility:N0} hidden) | " +
                $"{vehicles.CarHiddenAtOutsideConnection:N0} at OC | " +
                $"{vehicles.CarParkedOther:N0} other");
            report.AppendLine(
                $"Other: {vehicles.CarOtherLaneNull:N0} " +
                "no assigned parking lane (null lane) | " +
                $"{vehicles.CarOtherHiddenParkingLane:N0} hidden ParkingLane | " +
                $"{vehicles.CarOtherHiddenNonParkingLane:N0} hidden other lane | " +
                $"{vehicles.CarOtherVisibleNonParkingLane:N0} visible other lane");

            int spawnedWithoutLane =
                vehicles.CarParkedLaneNull -
                vehicles.CarParkedLaneNullUnspawned;

            report.AppendLine(
                $"No assigned lane: {vehicles.CarParkedLaneNull:N0} total | " +
                $"{vehicles.CarParkedLaneNullUnspawned:N0} unspawned | " +
                $"{spawnedWithoutLane:N0} spawned");

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
                $"{vehicles.CarOwnerMismatch:N0} total | " +
                $"{vehicles.CarMissingOwner:N0} missing Owner | " +
                $"{vehicles.CarOwnerMissingBuffer:N0} owner missing buffer | " +
                $"{vehicles.CarOwnerMissingBacklink:N0} backlink missing");
            report.AppendLine(
                "Game validates personal-vehicle ownership every 1,024 " +
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
                $"{vehicles.BicycleOwnerMismatch:N0} keeper mismatches");
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
                "Parked, not assigned (null lane)",
                vehicles.CarParkedLaneNullSamples);
            AppendEntityArray(
                report,
                "Owner mismatch",
                vehicles.CarOwnerMismatchSamples);
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
                "No assigned lane = ParkedCar with a null lane; " +
                "it is parked, not active or transitioning.");
            report.AppendLine(
                "A null lane alone does not mean abandoned. " +
                "Game's FixParkingLocationSystem can leave it null and add Unspawned " +
                "when no replacement parking space is found.");

            report.AppendLine(
                "Direct OC owner + DummyTraffic = normal game-created traffic.");
            report.AppendLine(
                "Direct OC owner without DummyTraffic = unexpected; inspect its Entity ID.");

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
    }
}

