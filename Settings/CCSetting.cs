// Settings/CCSetting.cs
namespace CitizenCleaner
{
    using System;                   // Exception, Action
    using System.Collections;       // IEnumerator (for NextFrame)

    using Colossal.IO.AssetDatabase;    // [FileLocation]
    using Colossal.Localization;

    using Game.Modding;             // IMod
    using Game.SceneFlow;           // GameManager
    using Game.Settings;            // ModSetting, [SettingsUI*]

    using UnityEngine;              // Application.OpenURL

    internal static class ModKeys
    {
        public const string SettingsKey = "CitizenCleaner";
    }

    [FileLocation("ModsSettings/CitizenEntityCleaner/CitizenEntityCleaner")]

    /// <summary>
    /// Settings UI attributes and backing fields
    /// </summary>
    [SettingsUITabOrder(MainTab, AboutTab, DebugTab)]
    [SettingsUIGroupOrder(kFiltersGroup, kButtonGroup, StatusGroup, InfoGroup, UsageGroup, DebugGroup)]
    [SettingsUIShowGroupName(kFiltersGroup, kButtonGroup, StatusGroup, DebugGroup)]  // InfoGroup + UsageGroup header omitted on purpose.

    public partial class CCSetting : ModSetting
    {
        #region UI Structure
        // ---- UI structure ----
        public const string kSection = "Main";
        public const string MainTab = "Main";
        public const string AboutTab = "About";
        public const string DebugTab = "Debug";
        public const string InfoGroup = "Info";
        public const string UsageGroup = "Usage";    //About tab section for usage instructions
        public const string kButtonGroup = "Button";
        public const string kFiltersGroup = "Filters";
        public const string StatusGroup = "VehicleStatus";
        public const string DebugGroup = "Debug";
        #endregion

        #region Defaults & Localization Keys
        // ---- UI text defaults ----
        private const string DefaultCountPrompt = "Click [Refresh Counts]";

        // ---- Localization (i18n) keys ----
        private const string RefreshPromptKey = "CitizenCleaner/Prompt/RefreshCounts";
        private const string NoCityKey = "CitizenCleaner/Prompt/NoCity";
        private const string ErrorKey = "CitizenCleaner/Prompt/Error";
        private const string StatusProgressKey = "CitizenCleaner/Status/Progress"; // "{0}" = P0 percent
        private const string StatusCleaningKey = "CitizenCleaner/Status/Cleaning"; // "{0}" = P0 percent

        private static string L(string key, string fallback)
        {
            LocalizationDictionary? dict = GameManager.instance?.localizationManager?.activeDictionary;
            return (dict != null && dict.TryGetValue(key, out var s) && !string.IsNullOrWhiteSpace(s))
                ? s
                : fallback;
        }
        #endregion

        #region Backing Fields and External Links
        // ---- External links ----
        private const string UrlParadoxMods = "https://mods.paradoxplaza.com/mods/117161/Windows";
        private const string UrlGitHub = "https://github.com/phillycheeze/CitizenEntityCleaner";
        private const string UrlDiscord = "https://discord.gg/HTav7ARPs2";  // Changed to main discord channel as direct channel doesn't work for non-members.

        // ---- Backing fields for UI ----
        private bool _includeCorrupt = true; // defaults ON
        private bool _includeMovingAwayNoPR = false;
        private bool _includeHomeless = false;
        private bool _includeCommuters = false;

        private string _totalCitizens = DefaultCountPrompt;
        private string _corruptedCitizens = DefaultCountPrompt;
        private bool _showRefreshPrompt = true;  // whether "Citizens to Clean" should show the prompt

        private string _cleanupStatus = "Idle";
        private bool _isCleanupInProgress = false;

        // runtime state flags to localize at read-time (avoid caching old language)
        private bool _showNoCity = false;
        private bool _showError = false;
        private bool _hasInitialCitySnapshot = false;
        #endregion

        /// <summary>
        /// Create settings object for this mod
        ///</summary>
        public CCSetting(IMod mod) : base(mod) { }

        #region Private Helpers
        // ---- Helpers ----
        private void ResetStatusIfNotRunning()
        {
            if (!_isCleanupInProgress)
                _cleanupStatus = "Idle";
        }

        // Mark 'Citizens to Clean' count as stale so the getter shows localized prompt until refreshed.
        private void ShowRefreshPrompt()
        {
            _showRefreshPrompt = true;   // show localized prompt until refreshed
            ApplyAndSave();
        }

        // Defer an action to next frame (lets confirmation modal finish opening/closing)
        private void NextFrame(Action action)
        {
            GameManager gm = GameManager.instance;
            if (gm != null) gm.StartCoroutine(NextFrameCo(action));
            else action?.Invoke(); // fallback if GM missing (unlikely in Options UI)
        }

        private IEnumerator NextFrameCo(Action action)
        {
            yield return null; // wait one frame
            action?.Invoke();
        }

        private void EnsureInitialCitySnapshot()
        {
            if (_hasInitialCitySnapshot || _isCleanupInProgress)
                return;

            // The first Options read in each city builds one on-demand snapshot.
            _hasInitialCitySnapshot = true;
            RefreshEntityCounts(applyChanges: false);
        }

        internal void InvalidateCitySnapshot()
        {
            _hasInitialCitySnapshot = false;
            _showNoCity = false;
            _showError = false;
            _showRefreshPrompt = true;
            _totalCitizens = DefaultCountPrompt;
            _corruptedCitizens = DefaultCountPrompt;
            _cleanupStatus = "Idle";
            ResetVehicleStatus();
        }
        #endregion

        #region Filter Toggles
        // ---- Filter & order of toggles ----
        [SettingsUISection(kSection, kFiltersGroup)]
        public bool IncludeCorrupt
        {
            get => _includeCorrupt;
            set
            {
                if (_includeCorrupt == value) return;
                _includeCorrupt = value;
                ResetStatusIfNotRunning();
                ShowRefreshPrompt();
            }
        }

        [SettingsUISection(kSection, kFiltersGroup)]
        public bool IncludeMovingAwayNoPR
        {
            get => _includeMovingAwayNoPR;
            set
            {
                if (_includeMovingAwayNoPR == value) return;
                _includeMovingAwayNoPR = value;
                ResetStatusIfNotRunning();
                ShowRefreshPrompt();
            }
        }

        [SettingsUISection(kSection, kFiltersGroup)]
        public bool IncludeCommuters
        {
            get => _includeCommuters;
            set
            {
                if (_includeCommuters == value) return;
                _includeCommuters = value;
                ResetStatusIfNotRunning();
                ShowRefreshPrompt();
            }
        }

        [SettingsUISection(kSection, kFiltersGroup)]
        public bool IncludeHomeless
        {
            get => _includeHomeless;
            set
            {
                if (_includeHomeless == value) return;
                _includeHomeless = value;
                ResetStatusIfNotRunning();
                ShowRefreshPrompt();
            }
        }
        #endregion

        #region Action Buttons
        // ---- Action Buttons ----
        [SettingsUIButton]
        [SettingsUISection(kSection, kButtonGroup)]
        public bool RefreshCountsButton
        {
            set
            {
                if (_isCleanupInProgress)
                {
                    Mod.log.Info("Cleanup in progress, ignoring refresh button click");
                    return;
                }

                Mod.log.Info("Refresh Counts button clicked");
                RefreshEntityCounts();
            }
        }

        /// <summary>
        /// Show a Yes/No confirmation. If Yes, wait one frame for the dialog to close cleanly
        /// Avoids focus conflict errors in UI.log
        /// </summary>
        [SettingsUIButton]
        [SettingsUIConfirmation]    // Yes/No modal
        [SettingsUISection(kSection, kButtonGroup)]
        public bool CleanupEntitiesButton
        {

            set
            {
                // If user clicks "No", value = false — bail early
                if (!value)
                {
#if DEBUG
        Mod.log.Debug("[Cleanup] Confirmation declined (No).");
#endif
                    return;
                }

                // Defer one frame so the confirmation modal can fully close before we change states.
                NextFrame(() =>
                {
                    try
                    {
                        if (_isCleanupInProgress)
                        {
                            Mod.log.Info("Cleanup already in progress, ignoring button click");
                            return;
                        }

                        CitizenCleanupSystem? cleanupSystem = Mod.CleanupSystem;
                        if (cleanupSystem == null)
                        {
                            Mod.log.Error("CleanupSystem was not initialized.");
                            return;
                        }

                        if (!cleanupSystem.HasAnyCitizenData())
                        {
                            Mod.log.Info("No city loaded (no citizen data). Load a city first");
                            return;
                        }

                        bool anySelected =
                            _includeCorrupt ||
                            _includeMovingAwayNoPR ||
                            _includeCommuters ||
                            _includeHomeless;
                        if (!anySelected)
                        {
                            Mod.log.Info("No checkboxes selected; nothing to clean.");
                            _cleanupStatus = "Nothing to clean";
                            _corruptedCitizens = "0";
                            _showRefreshPrompt = false;
                            Apply();
                            return;
                        }

                        _isCleanupInProgress = true;
                        _showRefreshPrompt = false;

                        Mod.log.Info("Cleanup Citizens confirmed YES; triggering cleanup (deferred)");
                        cleanupSystem.TriggerCleanup();
                    }
                    catch (Exception ex)
                    {
                        Mod.log.Error($"[Cleanup] Deferred start failed: {ex.GetType().Name}: {ex.Message}");
#if DEBUG
            Mod.log.Debug(ex.ToString());
#endif
                    }
                });
            }

        }
        #endregion

        #region Debug Button & Note
        // ---- Debug button ----
        [SettingsUIButton]
        [SettingsUISection(DebugTab, DebugGroup)]
        public bool LogDiagnosticReportButton
        {
            set
            {
                // Guard before work
                CitizenCleanupSystem? cleanupSystem = Mod.CleanupSystem;
                if (cleanupSystem == null)
                {
                    Mod.log.Error("CleanupSystem was not initialized.");
                    return;
                }

                if (!cleanupSystem.HasAnyCitizenData())
                {
                    Mod.log.Info("[Preview] No city loaded (no citizen data). Load a city first.");
                    return;
                }

                // One read-only report:
                //  - 25 corrupt citizen IDs
                //  - 10 moving-away, commuter, and homeless citizen IDs each
                //  - official 1.6.0 citizen counters
                //  - personal-car and bicycle status
                cleanupSystem.LogDiagnosticReportToLog();
            }
        }

        [SettingsUIMultilineText]
        [SettingsUISection(DebugTab, DebugGroup)]
        public string DebugReportNote => string.Empty;
        #endregion

        // OpenLog button
        [SettingsUIButton]
        [SettingsUISection(DebugTab, DebugGroup)]
        public bool OpenLogButton
        {
            set
            {
                // Open the mod log if present; otherwise open Logs folder.
                // Safe: no crash if the file/folder are missing or the shell fails.
                string logPath = Mod.LogFilePath;
                string? logsDirectory = System.IO.Path.GetDirectoryName(logPath);

                try
                {
                    string? path =
                        System.IO.File.Exists(logPath)
                            ? logPath
                            : logsDirectory;

                    if (string.IsNullOrEmpty(path) ||
                        (!System.IO.File.Exists(path) &&
                         !System.IO.Directory.Exists(path)))
                    {
                        Mod.log.Info("[Log] No log file or Logs folder found yet.");
                        return;
                    }

                    System.Diagnostics.Process.Start(
                        new System.Diagnostics.ProcessStartInfo(path)
                        {
                            UseShellExecute = true,
                            ErrorDialog = false,
                        });
                }
                catch (Exception ex)
                {
                    Mod.log.Warn($"[Log] Failed to open path: {ex.GetType().Name}: {ex.Message}");
                }
            }
        }

        #region Displays (Read-only)
        // ---- Read-only displays ----
        [SettingsUISection(kSection, kButtonGroup)]
        public string CleanupStatusDisplay => string.IsNullOrEmpty(_cleanupStatus) ? "Idle" : _cleanupStatus;

        [SettingsUISection(kSection, kButtonGroup)]
        public string TotalCitizensDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _showNoCity ? L(NoCityKey, "No city loaded")
                    : _showError ? L(ErrorKey, "Error")
                    : _totalCitizens;
            }
        }

        // If a language change occurs while UI is open, this getter re-reads the localized prompt.
        // Then, "Click [Refresh Counts]" reflects in current language without storing a stale string.
        [SettingsUISection(kSection, kButtonGroup)]
        public string CorruptedCitizensDisplay
        {
            get
            {
                EnsureInitialCitySnapshot();
                return _showNoCity ? L(NoCityKey, "No city loaded")
                    : _showError ? L(ErrorKey, "Error")
                    : _showRefreshPrompt
                        ? L(RefreshPromptKey, DefaultCountPrompt)
                        : _corruptedCitizens;
            }
        }
        #endregion

        #region About Tab
        // ---- About tab: info ----
        [SettingsUISection(AboutTab, InfoGroup)]
        public string NameText => Mod.Name;

        [SettingsUISection(AboutTab, InfoGroup)]
        public string VersionText => Mod.VersionShort;

#if DEBUG
        [SettingsUISection(AboutTab, InfoGroup)]
        public string InformationalVersionText => Mod.VersionInformational;
#endif

        // ---- About tab links: order below determines button order ----
        [SettingsUIButtonGroup("SocialLinks")]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, InfoGroup)]
        public bool OpenParadoxModsButton
        {
            set
            {
                try { Application.OpenURL(UrlParadoxMods); }
                catch (Exception ex) { Mod.log.Warn($"Failed to open Paradox Mods: {ex.Message}"); }
            }
        }

        [SettingsUIButtonGroup("SocialLinks")]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, InfoGroup)]
        public bool OpenGithubButton
        {
            set
            {
                try { Application.OpenURL(UrlGitHub); }
                catch (Exception ex) { Mod.log.Warn($"Failed to open GitHub: {ex.Message}"); }
            }
        }

        [SettingsUIButtonGroup("SocialLinks")]
        [SettingsUIButton]
        [SettingsUISection(AboutTab, InfoGroup)]
        public bool OpenDiscordButton
        {
            set
            {
                try { Application.OpenURL(UrlDiscord); }
                catch (Exception ex) { Mod.log.Warn($"Failed to open Discord: {ex.Message}"); }
            }
        }

        // ---- About tab: USAGE ----
        [SettingsUIMultilineText]
        [SettingsUISection(AboutTab, UsageGroup)]
        public string UsageSteps => string.Empty;

        [SettingsUIMultilineText]
        [SettingsUISection(AboutTab, UsageGroup)]
        public string UsageNotes => string.Empty;
        #endregion

        #region Defaults
        /// <summary>
        /// Initialize checkbox defaults and display text
        /// </summary>
        public override void SetDefaults()
        {
            // Checkbox defaults
            _includeCorrupt = true;
            _includeMovingAwayNoPR = false;
            _includeHomeless = false;
            _includeCommuters = false;

            // Display strings
            _totalCitizens = L(RefreshPromptKey, DefaultCountPrompt);
            _corruptedCitizens = L(RefreshPromptKey, DefaultCountPrompt);
            _cleanupStatus = "Idle";
            _showRefreshPrompt = true;   // show prompt until refreshed
            _showNoCity = false;
            _showError = false;
            _hasInitialCitySnapshot = false;
            ResetVehicleStatus();
        }
        #endregion

        #region Counts & Status Logic
        /// <summary>
        /// Update display values; handles errors
        /// </summary>
        public void RefreshEntityCounts()
        {
            RefreshEntityCounts(applyChanges: true);
        }

        private void RefreshEntityCounts(bool applyChanges)
        {
            _hasInitialCitySnapshot = true;

            try
            {
                // Treat missing system OR empty citizen data as “No city loaded”.
                CitizenCleanupSystem? cleanupSystem = Mod.CleanupSystem;
                if (cleanupSystem == null || !cleanupSystem.HasAnyCitizenData())
                {
                    _showNoCity = true;
                    _showError = false;
                    _totalCitizens = string.Empty;      // do not cache translated text
                    _corruptedCitizens = string.Empty;  // do not cache translated text

                    // No city data. Sticky “Complete”, otherwise stay idle.
                    if (!_isCleanupInProgress && _cleanupStatus != "Complete")
                        _cleanupStatus = "Idle";

                    _showRefreshPrompt = false; // show "No city" message, not the Refresh prompt
                    ResetVehicleStatus(noCity: true);
                    if (applyChanges)
                        Apply();

                    return;
                }


                // Data exists
                _showNoCity = false;
                _showError = false;
                (var totalCitizens, var citizensToClean) =
                    cleanupSystem.GetCitizenStatistics();

                _totalCitizens = $"{totalCitizens:N0}";
                _corruptedCitizens = $"{citizensToClean:N0}";
                RefreshVehicleStatus();

                // Don’t overwrite "Complete" immediately after a cleanup; keep it until filters change or a new cleanup runs.
                if (!_isCleanupInProgress && _cleanupStatus != "Complete")
                {
                    _cleanupStatus = citizensToClean > 0 ? "Idle" : "Nothing to clean";
                }
                _showRefreshPrompt = false; // display counts, not Refresh prompt

            }
            catch (Exception ex)
            {
                Mod.log.Warn($"Error refreshing entity counts: {ex.Message}");
                _showError = true;
                _showNoCity = false;

                // keep UI from getting stuck in an old status if an error happens
                if (!_isCleanupInProgress)
                    _cleanupStatus = "Idle";

                _totalCitizens = string.Empty;      // do not cache translated text
                _corruptedCitizens = string.Empty;  // do not cache translated text
                _showRefreshPrompt = false;         // show error, not Refresh prompt
                ResetVehicleStatus(error: true);
            }


            if (applyChanges)
                Apply();
        }
        #endregion

        #region Cleanup Progress & Completion
        /// <summary>
        /// Updates cleanup progress display
        /// </summary>
        public void UpdateCleanupProgress(float progress)
        {
            if (!_isCleanupInProgress)
            {
#if DEBUG
                Mod.log.Debug("Ignoring progress update because no cleanup is active.");
#endif
                return;
            }

            _showRefreshPrompt = false;
            var pct = progress.ToString("P0");
            _cleanupStatus = string.Format(L(StatusProgressKey, "Cleanup in progress… {0}"), pct);
            _corruptedCitizens = string.Format(L(StatusCleaningKey, "Cleaning… {0}"), pct);
            Apply();

        }

        /// <summary>
        /// Finishes progress tracking and refreshes final counts
        /// </summary>
        public void FinishCleanupProgress()
        {
            if (!_isCleanupInProgress)
            {
#if DEBUG
                Mod.log.Debug("FinishCleanupProgress called while not in progress; ignoring.");
#endif
                return;
            }

            _isCleanupInProgress = false;
            _cleanupStatus = "Complete";    // set first so Refresh won't change it
            RefreshEntityCounts();
        }

        /// <summary>
        /// Finishes progress when there was nothing to clean.
        /// </summary>
        public void FinishCleanupNoWork()
        {
            if (!_isCleanupInProgress)
            {
#if DEBUG
                Mod.log.Debug("FinishCleanupNoWork called while not in progress; ignoring.");
#endif
                return;
            }

            _isCleanupInProgress = false;
            _cleanupStatus = "Nothing to clean";
            RefreshEntityCounts();
        }
        #endregion

    }
}

