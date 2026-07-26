// LocaleEN.cs
using System.Collections.Generic;  // Dictionary
using Colossal;                    // IDictionarySource

namespace CitizenCleaner
{
    /// <summary>
    /// English locale entries (en-US)
    /// </summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly Setting m_Setting;
        public LocaleEN(Setting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(Setting.kSection), "Actions" },
                { m_Setting.GetOptionTabLocaleID(Setting.AboutTab), "About" },
                { m_Setting.GetOptionTabLocaleID(Setting.DebugTab), "Debug" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(Setting.kFiltersGroup), "Cleanup Targets" },
                { m_Setting.GetOptionGroupLocaleID(Setting.kButtonGroup), "Actions" },
                { m_Setting.GetOptionGroupLocaleID(Setting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(Setting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(Setting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncludeCorrupt)), "▪ Corrupt Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncludeCorrupt)),
                  "When enabled (default), counts and cleans up **corrupt** citizens;\n" +
                  "residents that lack a PropertyRenter component (and are not homeless, commuters, tourists, or moving-away).\n\n" +
                  "Corrupt citizens are the main target of this mod. If the city contains too many, it could cause problems over time." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncludeMovingAwayNoPR)), "▪ Moving-Away (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncludeMovingAwayNoPR)),
                  "When enabled, counts and cleans up citizens currently **Moving-Away** with Rent = 0 (i.e., no PropertyRenter component).\n\n" +
                  "Moving-Away citizens with PropertyRenter or Rent > 0 are not removed." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncludeCommuters)), "▪ Commuters" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncludeCommuters)),
                  "When enabled, counts and cleans up **commuter** citizens. Commuters include citizens that don't live in your city but travel to your city for work.\n\n" +
                  "Sometimes, commuters previously lived in your city but moved out due to homelessness (feature added in game version 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.IncludeHomeless)), "▪ Homeless" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.IncludeHomeless)),
                  "When enabled, counts and cleans up **homeless** citizens.\n\n" +
                  "<BE CAREFUL>: deleting homeless can cause unknown side effects." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CleanupEntitiesButton)), "Cleanup Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CleanupEntitiesButton)),
                  "Load a saved city first.\nRemoves citizens from households that no longer have a PropertyRenter component.\n" +
                  "Cleanup also includes any optional items selected [ ✓ ].\n\n" +
                  "**BE CAREFUL**: this is a workaround and may corrupt other data. Create a backup of your save first!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(Setting.CleanupEntitiesButton)),
                  "Permanently delete items selected in options.\n\nPlease backup your save first!\n Continue?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.RefreshCountsButton)), "Refresh Counts" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.RefreshCountsButton)),
                  "<Load a saved city first to get numbers.>\n" +
                  "Updates all entity counts to show current city statistics.\n" +
                  "After cleaning, let the game run unpaused for a minute." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CleanupStatusDisplay)), "Status" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CleanupStatusDisplay)),
                  "Shows the cleanup status. Updates live during an active cleanup; otherwise press [Refresh Counts] to recompute.\n\n" +
                  "\"**Idle**\" = no cleanup running or no city loaded yet.\n" +
                  "\"**Nothing to clean**\" = no citizens match the selected filters (or you already removed them).\n" +
                  "\"**Complete**\" = last cleanup finished; persists until you change filters or run a new cleanup." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.TotalCitizensDisplay)), "Total Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.TotalCitizensDisplay)),
                  "Total number of citizen entities **currently in the simulation.**\n\n" +
                  "This number can differ from your population because it may include corrupt entities." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CorruptedCitizensDisplay)),
                  "Citizens to Clean: select [ ✓ ] above" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CorruptedCitizensDisplay)),
                  "Number of citizen entities to remove when you click **[Cleanup]**,\n\n" +
                  "based on the selected boxes [ ✓ ]." },

                // Read-only status rows
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.CitizenCountComparisonDisplay)), "Citizen Count Comparison" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.CitizenCountComparisonDisplay)),
                  "Compares Citizen Cleaner's broad HouseholdMember entity count with the game's 1.6.0 valid moved-in citizen count. " +
                  "They are not expected to match because the game count excludes commuters, tourists, moving-away, invalid, and other non-resident entities." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PersonalCarStatusDisplay)), "Personal Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PersonalCarStatusDisplay)),
                  "All non-deleted PersonalCar entities except bicycles and trailers." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.PersonalCarParkingDisplay)), "Personal-Car Parking" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.PersonalCarParkingDisplay)),
                  "Exclusive parked buckets. Street uses a visible ParkingLane; facility follows the parked lane's owner chain to a GarageLane, ParkingFacility, CarParkingFacility, or Building; OC hidden uses Unspawned plus an OC parked lane or TripSource." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OutsideConnectionOwnerDisplay)), "OC-Hidden Car Owners" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OutsideConnectionOwnerDisplay)),
                  "Breaks OC-hidden cars down by owner location/type and also shows broken ownership backlinks. " +
                  "Owner at OC is diagnostic only; commuters and moving-away households can be legitimate transitions." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OutsideConnectionStageDisplay)), "OC-Hidden Staging Evidence" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OutsideConnectionStageDisplay)),
                  "Shows whether an OC-hidden car is linked by its parked lane or TripSource. " +
                  "TripSource at OC with no parked lane matches the game's initial no-nearby-parking fallback and is not proof of abandonment." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BicycleStatusDisplay)), "Bicycles" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BicycleStatusDisplay)),
                  "Bicycles are PersonalCar entities in the game, but are displayed separately because bicycle parking uses different infrastructure." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.BicycleParkingDisplay)), "Bicycle Parking" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.BicycleParkingDisplay)),
                  "Separates visible parked bicycles from hidden bicycles at outside connections or elsewhere." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VehicleOwnershipDisplay)), "Potential Orphans" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VehicleOwnershipDisplay)),
                  "Point-in-time ownership backlink mismatches using the same rules as the game's PersonalCarOwnerSystem. " +
                  "The game normally removes these, so a small temporary count is possible." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VehicleSnapshotTimeDisplay)), "Updated" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VehicleSnapshotTimeDisplay)),
                  "Vehicle data is scanned only when you press [Refresh Counts] or write the debug report; there is no per-frame status scan." },

                // Prompts (used by Setting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Click [Refresh Counts]" },
                { "CitizenCleaner/Prompt/NoCity", "No city loaded" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Cleanup in progress… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Cleaning… {0}" },
                { "CitizenCleaner/Status/CitizenCountRow",
                  "CC household-member entities {0} | game valid moved-in citizens {1} | difference {2}" },
                { "CitizenCleaner/Status/CarSummaryRow",
                  "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/CarParkingRow",
                  "Street {0} | building/parking facility {1} (hidden {2}) | OC hidden {3} | other {4} (hidden {5})" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow",
                  "City household {0} | owner at OC {1} | nonresident/moving {2} | missing/non-household {3} | broken backlink {4}" },
                { "CitizenCleaner/Status/OcHiddenStageRow",
                  "OC evidence: parked lane {0} | TripSource {1} | TripSource with no lane {2} | HomeTarget {3} | keeper at OC {4}" },
                { "CitizenCleaner/Status/BicycleSummaryRow",
                  "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/BicycleParkingRow",
                  "Visible parked {0} | OC hidden {1} | hidden elsewhere {2}" },
                { "CitizenCleaner/Status/OwnershipRow",
                  "Ownership mismatches: personal cars {0} | bicycles {1}" },
                { "CitizenCleaner/Status/CapturedAtRow", "Snapshot time {0}" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.NameText)), "Mod Name" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.NameText)), "Display name of this mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.VersionText)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.VersionText)), "Current mod version." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.InformationalVersionText)), "Informational Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.InformationalVersionText)), "Mod Version with Commit ID" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenGithubButton)),   "GitHub repository for the mod; opens in browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenDiscordButton)),  "Discord chat for feedback on the mod; opens in browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenParadoxModsButton)),  "Paradox Mods website; opens in browser." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(Setting.UsageGroup), "USAGE" },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.UsageSteps)),
                  "1. <Backup your save file first!>\n" +
                  "2. <Click [Refresh Counts] to see current statistics.>\n" +
                  "3. [ ✓ ] <Select the items to include using the checkboxes>\n" +
                  "4. <Click [Cleanup Citizens] to clean up entities.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.UsageNotes)),
                  "Notes:\n" +
                  "• This mod does **not** run automatically; use **[Cleanup Citizens]** each time for removals.\n" +
                  "• Revert to original saved city if needed for unexpected behavior." },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.UsageNotes)), "" },


                 // Debug Tab — one read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.LogDiagnosticReportButton)), "Write Diagnostic Report to Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.LogDiagnosticReportButton)),
                  "- Writes one organized report with **25 corrupt IDs**, plus **10 moving-away, 10 commuter, and 10 homeless IDs** (Index:Version).\n\n" +
                  "- Also includes the game/CC citizen-count comparison and personal-car/bicycle status.\n\n" +
                  "- **Read-only** — does not delete anything.\n\n" +
                  "- Log file at:\n" +
                  "%USERPROFILE%/AppData/LocalLow/Colossal Order/Cities Skylines II/logs/CitizenCleaner.log" },

                // Sentence UNDER the button (multiline text row)
                // LabelLocale = inline body under the button
                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.DebugReportNote)),
                  "One button writes the complete, readable troubleshooting report. Nothing is deleted." },

                { m_Setting.GetOptionLabelLocaleID(nameof(Setting.OpenLogButton)), "Open Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(Setting.OpenLogButton)), "Open the log file in the default text editor." },

            };
        }
        public void Unload() { }
    }
}
