// Setting.Status.cs
using System;
using Game.Settings;

namespace CitizenCleaner
{
    /// <summary>
    /// Read-only citizen-count comparison and personal-vehicle status rows.
    /// Values change only when Refresh Counts is pressed.
    /// </summary>
    public partial class Setting
    {
        private const string CitizenCountRowKey = "CitizenCleaner/Status/CitizenCountRow";
        private const string CarSummaryRowKey = "CitizenCleaner/Status/CarSummaryRow";
        private const string CarParkingRowKey = "CitizenCleaner/Status/CarParkingRow";
        private const string OcHiddenOwnerRowKey = "CitizenCleaner/Status/OcHiddenOwnerRow";
        private const string OcHiddenStageRowKey = "CitizenCleaner/Status/OcHiddenStageRow";
        private const string BicycleSummaryRowKey = "CitizenCleaner/Status/BicycleSummaryRow";
        private const string BicycleParkingRowKey = "CitizenCleaner/Status/BicycleParkingRow";
        private const string OwnershipRowKey = "CitizenCleaner/Status/OwnershipRow";
        private const string CapturedAtRowKey = "CitizenCleaner/Status/CapturedAtRow";

        private const string CitizenCountRowFallback =
            "CC household-member entities {0} | game valid moved-in citizens {1} | difference {2}";
        private const string CarSummaryRowFallback =
            "Total {0} | active {1} | parked {2} | transitioning/other {3}";
        private const string CarParkingRowFallback =
            "Street {0} | building/parking facility {1} (hidden {2}) | OC hidden {3} | other {4} (hidden {5})";
        private const string OcHiddenOwnerRowFallback =
            "City household {0} | owner at OC {1} | nonresident/moving {2} | missing/non-household {3} | broken backlink {4}";
        private const string OcHiddenStageRowFallback =
            "OC evidence: parked lane {0} | TripSource {1} | TripSource with no lane {2} | HomeTarget {3} | keeper at OC {4}";
        private const string BicycleSummaryRowFallback =
            "Total {0} | active {1} | parked {2} | transitioning/other {3}";
        private const string BicycleParkingRowFallback =
            "Visible parked {0} | OC hidden {1} | hidden elsewhere {2}";
        private const string OwnershipRowFallback =
            "Ownership mismatches: personal cars {0} | bicycles {1}";
        private const string CapturedAtRowFallback = "Snapshot time {0}";

        private string _citizenCountComparison = DefaultCountPrompt;
        private string _personalCarSummary = DefaultCountPrompt;
        private string _personalCarParking = DefaultCountPrompt;
        private string _ocHiddenOwners = DefaultCountPrompt;
        private string _ocHiddenStage = DefaultCountPrompt;
        private string _bicycleSummary = DefaultCountPrompt;
        private string _bicycleParking = DefaultCountPrompt;
        private string _vehicleOwnership = DefaultCountPrompt;
        private string _vehicleSnapshotTime = DefaultCountPrompt;

        [SettingsUISection(kSection, StatusGroup)]
        public string CitizenCountComparisonDisplay => _citizenCountComparison;

        [SettingsUISection(kSection, StatusGroup)]
        public string PersonalCarStatusDisplay => _personalCarSummary;

        [SettingsUISection(kSection, StatusGroup)]
        public string PersonalCarParkingDisplay => _personalCarParking;

        [SettingsUISection(kSection, StatusGroup)]
        public string OutsideConnectionOwnerDisplay => _ocHiddenOwners;

        [SettingsUISection(kSection, StatusGroup)]
        public string OutsideConnectionStageDisplay => _ocHiddenStage;

        [SettingsUISection(kSection, StatusGroup)]
        public string BicycleStatusDisplay => _bicycleSummary;

        [SettingsUISection(kSection, StatusGroup)]
        public string BicycleParkingDisplay => _bicycleParking;

        [SettingsUISection(kSection, StatusGroup)]
        public string VehicleOwnershipDisplay => _vehicleOwnership;

        [SettingsUISection(kSection, StatusGroup)]
        public string VehicleSnapshotTimeDisplay => _vehicleSnapshotTime;

        private void ResetVehicleStatus(bool noCity = false, bool error = false)
        {
            string message =
                noCity ? L(NoCityKey, "No city loaded") :
                error ? L(ErrorKey, "Error") :
                L(RefreshPromptKey, DefaultCountPrompt);

            _citizenCountComparison = message;
            _personalCarSummary = message;
            _personalCarParking = message;
            _ocHiddenOwners = message;
            _ocHiddenStage = message;
            _bicycleSummary = message;
            _bicycleParking = message;
            _vehicleOwnership = message;
            _vehicleSnapshotTime = message;
        }

        private void RefreshVehicleStatus()
        {
            CitizenCleanupSystem cleanupSystem = Mod.CleanupSystem
                ?? throw new InvalidOperationException("CitizenCleanupSystem is not initialized.");
            CitizenVehicleStatusSystem vehicleSystem = Mod.VehicleStatusSystem
                ?? throw new InvalidOperationException("CitizenVehicleStatusSystem is not initialized.");

            CitizenCleanupSystem.CitizenCountSnapshot citizenCounts =
                cleanupSystem.GetCitizenCountSnapshot();
            CitizenVehicleStatusSystem.Snapshot vehicles =
                vehicleSystem.BuildSnapshot();

            int difference =
                citizenCounts.CCHouseholdMemberEntities -
                citizenCounts.GameValidMovedInCitizens;

            _citizenCountComparison = string.Format(
                L(CitizenCountRowKey, CitizenCountRowFallback),
                FormatCount(citizenCounts.CCHouseholdMemberEntities),
                FormatCount(citizenCounts.GameValidMovedInCitizens),
                FormatSignedCount(difference));

            _personalCarSummary = string.Format(
                L(CarSummaryRowKey, CarSummaryRowFallback),
                FormatCount(vehicles.CarTotal),
                FormatCount(vehicles.CarActive),
                FormatCount(vehicles.CarParked),
                FormatCount(vehicles.CarTransitioning));

            _personalCarParking = string.Format(
                L(CarParkingRowKey, CarParkingRowFallback),
                FormatCount(vehicles.CarParkedOnStreet),
                FormatCount(vehicles.CarParkedAtFacility),
                FormatCount(vehicles.CarHiddenAtFacility),
                FormatCount(vehicles.CarHiddenAtOutsideConnection),
                FormatCount(vehicles.CarParkedOther),
                FormatCount(vehicles.CarHiddenOther));

            _ocHiddenOwners = string.Format(
                L(OcHiddenOwnerRowKey, OcHiddenOwnerRowFallback),
                FormatCount(vehicles.CarOcHiddenCityHouseholdOwner),
                FormatCount(vehicles.CarOcHiddenOwnerAtOutsideConnection),
                FormatCount(vehicles.CarOcHiddenNonResidentOrMovingOwner),
                FormatCount(vehicles.CarOcHiddenMissingOrNonHouseholdOwner),
                FormatCount(vehicles.CarOcHiddenOwnershipMismatch));

            _ocHiddenStage = string.Format(
                L(OcHiddenStageRowKey, OcHiddenStageRowFallback),
                FormatCount(vehicles.CarOcHiddenLaneAtOutsideConnection),
                FormatCount(vehicles.CarOcHiddenTripSourceAtOutsideConnection),
                FormatCount(vehicles.CarOcHiddenTripSourceWithoutLane),
                FormatCount(vehicles.CarOcHiddenHomeTarget),
                FormatCount(vehicles.CarOcHiddenKeeperAtOutsideConnection));

            _bicycleSummary = string.Format(
                L(BicycleSummaryRowKey, BicycleSummaryRowFallback),
                FormatCount(vehicles.BicycleTotal),
                FormatCount(vehicles.BicycleActive),
                FormatCount(vehicles.BicycleParked),
                FormatCount(vehicles.BicycleTransitioning));

            _bicycleParking = string.Format(
                L(BicycleParkingRowKey, BicycleParkingRowFallback),
                FormatCount(vehicles.BicycleVisibleParked),
                FormatCount(vehicles.BicycleHiddenAtOutsideConnection),
                FormatCount(vehicles.BicycleHiddenOther));

            _vehicleOwnership = string.Format(
                L(OwnershipRowKey, OwnershipRowFallback),
                FormatCount(vehicles.CarOwnershipMismatch),
                FormatCount(vehicles.BicycleOwnershipMismatch));

            _vehicleSnapshotTime = string.Format(
                L(CapturedAtRowKey, CapturedAtRowFallback),
                vehicles.CapturedAt.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        private static string FormatCount(int value) => value.ToString("N0");

        private static string FormatSignedCount(int value)
        {
            return value > 0
                ? "+" + value.ToString("N0")
                : value.ToString("N0");
        }
    }
}
