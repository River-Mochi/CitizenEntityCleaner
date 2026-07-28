// Settings/CCSetting.Status.cs
namespace CitizenCleaner
{
    using System;
    using Game.Settings;

    /// <summary>
    /// Compact personal-car status for the Options menu.
    /// Full details are written to the diagnostic report.
    /// </summary>
    public partial class CCSetting
    {
        private const string StatusButtonsRow = "StatusButtonsRow";

        private const string CarSummaryRowKey =
            "CitizenCleaner/Status/CarSummaryRow";
        private const string CarParkingRowKey =
            "CitizenCleaner/Status/CarParkingRow";
        private const string OcHiddenOwnerRowKey =
            "CitizenCleaner/Status/OcHiddenOwnerRow";
        private const string OwnershipRowKey =
            "CitizenCleaner/Status/OwnershipRow";

        private const string CarSummaryRowFallback =
            "{0} active | {1} parked | {2} total";
        private const string CarParkingRowFallback =
            "{0} street | {1} facility | {2} OC hidden | {3} other";
        private const string OcHiddenOwnerRowFallback =
            "{0} city | {1} OC | {2} away | {3} missing | updated {4}";
        private const string OwnershipRowFallback =
            "{0} total | {1} street | {2} facility | {3} at OC";

        private string _personalCarSummary = DefaultCountPrompt;
        private string _personalCarParking = DefaultCountPrompt;
        private string _ocHiddenOwners = DefaultCountPrompt;
        private string _vehicleOwnership = DefaultCountPrompt;

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
        public string VehicleOwnershipDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _vehicleOwnership;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        [SettingsUIButtonGroup(StatusButtonsRow)]
        [SettingsUIButton]
        public bool LogStatusReportButton
        {
            set
            {
                if (value)
                    LogDiagnosticReportButton = true;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        [SettingsUIButtonGroup(StatusButtonsRow)]
        [SettingsUIButton]
        public bool OpenLogFromStatusButton
        {
            set
            {
                if (value)
                    OpenLogButton = true;
            }
        }

        private void ResetVehicleStatus(bool noCity = false, bool error = false)
        {
            string message =
                noCity ? L(NoCityKey, "No city loaded") :
                error ? L(ErrorKey, "Error") :
                L(RefreshPromptKey, DefaultCountPrompt);

            _personalCarSummary = message;
            _personalCarParking = message;
            _ocHiddenOwners = message;
            _vehicleOwnership = message;
        }

        private void RefreshVehicleStatus()
        {
            CitizenVehicleStatusSystem vehicleSystem = Mod.VehicleStatusSystem
                ?? throw new InvalidOperationException(
                    "CitizenVehicleStatusSystem is not initialized.");

            CitizenVehicleStatusSystem.Snapshot vehicles =
                vehicleSystem.BuildSnapshot();

            _personalCarSummary = string.Format(
                L(CarSummaryRowKey, CarSummaryRowFallback),
                FormatCount(vehicles.CarActive),
                FormatCount(vehicles.CarParked),
                FormatCount(vehicles.CarTotal));

            _personalCarParking = string.Format(
                L(CarParkingRowKey, CarParkingRowFallback),
                FormatCount(vehicles.CarParkedOnStreet),
                FormatCount(vehicles.CarParkedAtFacility),
                FormatCount(vehicles.CarHiddenAtOutsideConnection),
                FormatCount(vehicles.CarParkedOther));

            int atOutsideConnection =
                vehicles.CarOcHiddenHouseholdAtOutsideConnection +
                vehicles.CarOcHiddenDirectOutsideConnectionOwner;

            _ocHiddenOwners = string.Format(
                L(OcHiddenOwnerRowKey, OcHiddenOwnerRowFallback),
                FormatCount(vehicles.CarOcHiddenCityHouseholdOwner),
                FormatCount(atOutsideConnection),
                FormatCount(vehicles.CarOcHiddenNonResidentOrMovingOwner),
                FormatCount(vehicles.CarOcHiddenMissingOrNonHouseholdOwner),
                vehicles.CapturedAt.ToString("HH:mm:ss"));

            _vehicleOwnership = string.Format(
                L(OwnershipRowKey, OwnershipRowFallback),
                FormatCount(vehicles.CarOwnershipMismatch),
                FormatCount(vehicles.CarStreetOwnershipMismatch),
                FormatCount(vehicles.CarFacilityOwnershipMismatch),
                FormatCount(vehicles.CarOcHiddenOwnershipMismatch));
        }

        private static string FormatCount(int value) => value.ToString("N0");

        // Temporary compatibility properties until all locale files are aligned.
        [SettingsUIHidden]
        public string CitizenCountComparisonDisplay => string.Empty;

        [SettingsUIHidden]
        public string OutsideConnectionStageDisplay => string.Empty;

        [SettingsUIHidden]
        public string BicycleStatusDisplay => string.Empty;

        [SettingsUIHidden]
        public string BicycleParkingDisplay => string.Empty;

        [SettingsUIHidden]
        public string VehicleOwnershipLocationDisplay => string.Empty;

        [SettingsUIHidden]
        public string VehicleSnapshotTimeDisplay => string.Empty;

    }
}
