namespace CitizenCleaner
{
    using System.Reflection;

    using Colossal.IO.AssetDatabase;
    using Colossal.Localization;
    using Colossal.Logging;
    using Colossal.PSI.Environment;

    using Game;
    using Game.Modding;
    using Game.SceneFlow;

    public class Mod : IMod
    {
        private const string kLogId = "CitizenCleaner";
        private static readonly Assembly s_asm = Assembly.GetExecutingAssembly();
        private static readonly string s_versionInformationalRaw =
            s_asm.GetCustomAttribute<AssemblyInformationalVersionAttribute>()
                ?.InformationalVersion ?? "1.0.0";
        private static bool s_bannerLogged;

        public static readonly string Name =
            s_asm.GetCustomAttribute<AssemblyTitleAttribute>()?.Title
                ?? "Citizen Cleaner";
        public static readonly string VersionShort =
            s_versionInformationalRaw.Split(' ', '+')[0];
        public static readonly string VersionInformational =
            s_versionInformationalRaw;
        public static readonly ILog log = LogManager
            .GetLogger(kLogId)
            .SetShowsErrorsInUI(false);

        public static string LogFilePath =>
            $"{EnvPath.kUserDataPath}/Logs/{kLogId}.log";

        public static CCSetting? Settings { get; private set; }
        public static CitizenCleanupSystem? CleanupSystem { get; private set; }
        public static CitizenVehicleStatusSystem? VehicleStatusSystem { get; private set; }

        public void OnLoad(UpdateSystem updateSystem)
        {
            if (!s_bannerLogged)
            {
                log.Info(
                    $"Mod: {Name} | Version: {VersionShort} | " +
                    $"Info: {VersionInformational}");
                s_bannerLogged = true;
            }

            CCSetting setting = new CCSetting(this);
            Settings = setting;

            LocalizationManager? localizationManager =
                GameManager.instance?.localizationManager;

            if (localizationManager != null)
            {
                localizationManager.AddSource("en-US", new LocaleEN(setting));
                localizationManager.AddSource("fr-FR", new LocaleFR(setting));
                localizationManager.AddSource("es-ES", new LocaleES(setting));
                localizationManager.AddSource("de-DE", new LocaleDE(setting));
                localizationManager.AddSource("it-IT", new LocaleIT(setting));
                localizationManager.AddSource("ja-JP", new LocaleJA(setting));
                localizationManager.AddSource("ko-KR", new LocaleKO(setting));
                localizationManager.AddSource("vi-VN", new LocaleVI(setting));
                localizationManager.AddSource("pl-PL", new LocalePL(setting));
                localizationManager.AddSource("pt-BR", new LocalePT_BR(setting));
                localizationManager.AddSource("zh-HANS", new LocaleZH_HANS(setting));
                localizationManager.AddSource("zh-HANT", new LocaleZH_HANT(setting));

#if DEBUG
                log.Debug(
                    $"[Locale] Active at load: " +
                    $"{localizationManager.activeLocaleId}");
#endif
            }
            else
            {
                log.Warn("Localization manager not available.");
            }

            AssetDatabase.global.LoadSettings( ModKeys.SettingsKey, setting, new CCSetting(this));
            setting.RegisterInOptionsUI();

            updateSystem.UpdateAt<CitizenCleanupSystem>( SystemUpdatePhase.Modification1);

            CleanupSystem = updateSystem.World
                    .GetOrCreateSystemManaged<CitizenCleanupSystem>();
            CleanupSystem.SetSettings(setting);

            VehicleStatusSystem = updateSystem.World
                    .GetOrCreateSystemManaged<CitizenVehicleStatusSystem>();

            CleanupSystem.OnCleanupProgress += setting.UpdateCleanupProgress;
            CleanupSystem.OnCleanupCompleted += setting.FinishCleanupProgress;
            CleanupSystem.OnCleanupNoWork += setting.FinishCleanupNoWork;
        }

        // Unsubscribe event handlers and unregister Options UI
        public void OnDispose()
        {
            log.Info(nameof(OnDispose));

            CCSetting? setting = Settings;
            CitizenCleanupSystem? cleanupSystem = CleanupSystem;

            if (setting != null && cleanupSystem != null)
            {
                cleanupSystem.OnCleanupProgress -= setting.UpdateCleanupProgress;
                cleanupSystem.OnCleanupCompleted -= setting.FinishCleanupProgress;
                cleanupSystem.OnCleanupNoWork -= setting.FinishCleanupNoWork;
            }

            setting?.UnregisterInOptionsUI();

            CleanupSystem = null;
            VehicleStatusSystem = null;
            Settings = null;
        }
    }
}
