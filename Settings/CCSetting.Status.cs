// Settings/CCSetting.Status.cs
namespace CitizenCleaner
{
    using System;
    using Game.Settings;

    /// <summary>
    /// Compact housing and personal-car status for the Options menu.
    /// Full details are written to the diagnostic report.
    /// </summary>
    public partial class CCSetting
    {
        private const string StatusButtonsRow = "StatusButtonsRow";

        private const string CarSummaryRowKey =
            "CitizenCleaner/Status/CarSummaryRowV2";
        private const string HousingRowKey =
            "CitizenCleaner/Status/HousingRowV1";
        private const string CarParkingRowKey =
            "CitizenCleaner/Status/CarParkingRowV2";
        private const string OcHiddenOwnerRowKey =
            "CitizenCleaner/Status/OcHiddenOwnerRowV2";

        private const string CarSummaryRowFallback =
            "{0} active | {1} parked | {2} total | updated {3}";
        private const string HousingRowFallback =
            "{0} seeking | {1} moving in | {2} moving out";
        private const string CarParkingRowFallback =
            "{0} street | {1} facility | {2} OC | {3} other";
        private const string OcHiddenOwnerRowFallback =
            "{0} city | {1} at OC | {2} OC owner | {3} away | {4} missing";

        private string m_Housing = DefaultCountPrompt;
        private string m_CarSummary = DefaultCountPrompt;
        private string m_ParkedCars = DefaultCountPrompt;
        private string m_CarsAtOutsideConnection = DefaultCountPrompt;

        [SettingsUISection(kSection, StatusGroup)]
        public string StatusHousing
        {
            get
            {
                EnsureInitialCitySnapshot();
                return m_Housing;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string StatusCars
        {
            get
            {
                EnsureInitialCitySnapshot();
                return m_CarSummary;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string StatusParkedCars
        {
            get
            {
                EnsureInitialCitySnapshot();
                return m_ParkedCars;
            }
        }

        [SettingsUISection(kSection, StatusGroup)]
        public string StatusHiddenAtOc
        {
            get
            {
                EnsureInitialCitySnapshot();
                return m_CarsAtOutsideConnection;
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

            m_Housing = message;
            m_CarSummary = message;
            m_ParkedCars = message;
            m_CarsAtOutsideConnection = message;
        }

        private void RefreshVehicleStatus()
        {
            CitizenCleanupSystem cleanupSystem = Mod.CleanupSystem
                ?? throw new InvalidOperationException(
                    "CitizenCleanupSystem is not initialized.");
            CitizenVehicleStatusSystem vehicleSystem = Mod.VehicleStatusSystem
                ?? throw new InvalidOperationException(
                    "CitizenVehicleStatusSystem is not initialized.");

            CitizenCleanupSystem.HousingStatusSnapshot housing =
                cleanupSystem.GetHousingStatusSnapshot();

            m_Housing = housing.GameCountsReady
                ? string.Format(
                    L(HousingRowKey, HousingRowFallback),
                    FormatCount(housing.PropertySeekerHouseholds),
                    FormatCount(housing.MovingInHouseholds),
                    FormatCount(housing.MovingAwayHouseholds))
                : L(
                    "CitizenCleaner/Report/GameCountsPending",
                    "Game counts are still initializing.");

            CitizenVehicleStatusSystem.Snapshot vehicles =
                vehicleSystem.BuildSnapshot();

            m_CarSummary = string.Format(
                L(CarSummaryRowKey, CarSummaryRowFallback),
                FormatCount(vehicles.CarActive),
                FormatCount(vehicles.CarParked),
                FormatCount(vehicles.CarTotal),
                vehicles.CapturedAt.ToString("HH:mm:ss"));

            m_ParkedCars = string.Format(
                L(CarParkingRowKey, CarParkingRowFallback),
                FormatCount(vehicles.CarParkedOnStreet),
                FormatCount(vehicles.CarParkedAtFacility),
                FormatCount(vehicles.CarHiddenAtOutsideConnection),
                FormatCount(vehicles.CarParkedOther));

            m_CarsAtOutsideConnection = string.Format(
                L(OcHiddenOwnerRowKey, OcHiddenOwnerRowFallback),
                FormatCount(vehicles.CarOcHiddenCityHouseholdOwner),
                FormatCount(vehicles.CarOcHiddenHouseholdAtOutsideConnection),
                FormatCount(vehicles.CarOcHiddenDirectOutsideConnectionOwner),
                FormatCount(vehicles.CarOcHiddenNonResidentOrMovingOwner),
                FormatCount(vehicles.CarOcHiddenMissingOrNonHouseholdOwner));
        }

        private static string FormatCount(int value) => value.ToString("N0");
    }
}
