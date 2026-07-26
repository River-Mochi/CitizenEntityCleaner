// CCSetting.Status.cs
using System;
using Game.Settings;

namespace CitizenCleaner
{
    /// <summary>
    /// Read-only citizen-count comparison and personal-vehicle status rows.
    /// Builds once on first display, then updates with Refresh Counts.
    /// </summary>
    public partial class CCSetting
    {
        private const string CitizenCountRowKey = "CitizenCleaner/Status/CitizenCountRow";
        private const string CitizenCountPendingRowKey = "CitizenCleaner/Status/CitizenCountPendingRow";
        private const string CarSummaryRowKey = "CitizenCleaner/Status/CarSummaryRow";
        private const string CarParkingRowKey = "CitizenCleaner/Status/CarParkingRow";
        private const string OcHiddenOwnerRowKey = "CitizenCleaner/Status/OcHiddenOwnerRow";
        private const string OcHiddenStageRowKey = "CitizenCleaner/Status/OcHiddenStageRow";
        private const string BicycleSummaryRowKey = "CitizenCleaner/Status/BicycleSummaryRow";
        private const string BicycleParkingRowKey = "CitizenCleaner/Status/BicycleParkingRow";
        private const string OwnershipRowKey = "CitizenCleaner/Status/OwnershipRow";
        private const string OwnershipLocationRowKey = "CitizenCleaner/Status/OwnershipLocationRow";
        private const string CapturedAtRowKey = "CitizenCleaner/Status/CapturedAtRow";

        private const string CitizenCountRowFallback =
            "CC household-member entities {0} | game valid moved-in citizens {1} | difference {2}";
        private const string CitizenCountPendingRowFallback =
            "CC household-member entities {0} | game counts are still initializing";
        private const string CarSummaryRowFallback =
            "Total {0} | active {1} | parked {2} | transitioning/other {3}";
        private const string CarParkingRowFallback =
            "Street {0} | building/parking facility {1} (hidden {2}) | OC hidden {3} | other {4} (hidden {5})";
        private const string OcHiddenOwnerRowFallback =
            "City household {0} | household at OC {1} | direct OC owner {2} | nonresident/moving {3} | missing/non-household {4} | ownership mismatch {5}";
        private const string OcHiddenStageRowFallback =
            "OC evidence: parked lane {0} | TripSource {1} | TripSource with no lane {2} | HomeTarget {3} | keeper at OC {4}";
        private const string BicycleSummaryRowFallback =
            "Total {0} | active {1} | parked {2} | transitioning/other {3}";
        private const string BicycleParkingRowFallback =
            "Visible parked {0} | OC hidden {1} | hidden elsewhere {2}";
        private const string OwnershipRowFallback =
            "Cars {0}: no Owner {1} | owner has no buffer {2} | backlink missing {3} | bicycles {4}";
        private const string OwnershipLocationRowFallback =
            "Parked mismatches: street {0} | building/parking facility {1} | OC hidden {2} | other {3}";
        private const string CapturedAtRowFallback = "Snapshot time {0}";

        private string _citizenCountComparison = DefaultCountPrompt;
        private string _personalCarSummary = DefaultCountPrompt;
        private string _personalCarParking = DefaultCountPrompt;
        private string _ocHiddenOwners = DefaultCountPrompt;
        private string _ocHiddenStage = DefaultCountPrompt;
        private string _bicycleSummary = DefaultCountPrompt;
        private string _bicycleParking = DefaultCountPrompt;
        private string _vehicleOwnership = DefaultCountPrompt;
        private string _vehicleOwnershipLocation = DefaultCountPrompt;
        private string _vehicleSnapshotTime = DefaultCountPrompt;

        [SettingsUISection(kSection, StatusGroup)]
        public string CitizenCountComparisonDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _citizenCountComparison;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string PersonalCarStatusDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _personalCarSummary;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string PersonalCarParkingDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _personalCarParking;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string OutsideConnectionOwnerDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _ocHiddenOwners;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string OutsideConnectionStageDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _ocHiddenStage;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string BicycleStatusDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _bicycleSummary;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string BicycleParkingDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _bicycleParking;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string VehicleOwnershipDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _vehicleOwnership;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string VehicleOwnershipLocationDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _vehicleOwnershipLocation;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string VehicleSnapshotTimeDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _vehicleSnapshotTime;
            }
        }

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
            _vehicleOwnershipLocation = message;
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

            _citizenCountComparison = citizenCounts.GameCountsReady
                ? string.Format(
                    L(CitizenCountRowKey, CitizenCountRowFallback),
                    FormatCount(citizenCounts.CCHouseholdMemberEntities),
                    FormatCount(citizenCounts.GameValidMovedInCitizens),
                    FormatSignedCount(difference))
                : string.Format(
                    L(CitizenCountPendingRowKey, CitizenCountPendingRowFallback),
                    FormatCount(citizenCounts.CCHouseholdMemberEntities));

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
                FormatCount(vehicles.CarOcHiddenHouseholdAtOutsideConnection),
                FormatCount(vehicles.CarOcHiddenDirectOutsideConnectionOwner),
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
                FormatCount(vehicles.CarMissingOwner),
                FormatCount(vehicles.CarOwnerMissingBuffer),
                FormatCount(vehicles.CarOwnerMissingBacklink),
                FormatCount(vehicles.BicycleOwnershipMismatch));

            _vehicleOwnershipLocation = string.Format(
                L(OwnershipLocationRowKey, OwnershipLocationRowFallback),
                FormatCount(vehicles.CarStreetOwnershipMismatch),
                FormatCount(vehicles.CarFacilityOwnershipMismatch),
                FormatCount(vehicles.CarOcHiddenOwnershipMismatch),
                FormatCount(vehicles.CarOtherParkedOwnershipMismatch));

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
