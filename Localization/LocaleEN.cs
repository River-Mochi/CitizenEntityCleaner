// Localization/LocaleEN.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// English locale (en-US)
    /// </summary>
    public class LocaleEN : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleEN(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Actions" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "About" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Debug" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Cleanup Targets" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Actions" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Corrupt Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "When enabled (default), counts **corrupt** citizens for [Cleanup Citizens].\n" +
                  "These citizens belong to households without PropertyRenter and are not homeless, commuters, tourists, or moving-away.\n\n" +
                  "- Corrupt citizens and abandoned cars are the main target of this mod.\n" +
                  "- The game's normal cleanup systems handle remaining references after CC marks a citizen for deletion.\n" +
                  "- If no household members remain, then personal vehicle should also be removed, freeing parking spaces." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ Moving-Away (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "When enabled, counts and cleans up citizens currently **Moving-Away** with Rent = 0 (i.e., no PropertyRenter component).\n\n" +
                  "Moving-Away citizens with PropertyRenter or Rent > 0 are not removed." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Commuters" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "When enabled, counts and cleans up **commuter** citizens. Commuters include citizens that don't live in your city but travel to your city for work.\n\n" +
                  "Sometimes, commuters previously lived in your city but moved out due to homelessness (feature added in game version 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Homeless" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "When enabled, counts and cleans up **homeless** citizens.\n\n" +
                  "<BE CAREFUL>: deleting homeless can cause unknown side effects." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Cleanup Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Load a saved city first.\nRemoves citizens from households that no longer have a PropertyRenter component.\n" +
                  "Cleanup also includes any optional items selected [ ✓ ].\n\n" +
                  "**BE CAREFUL**: this is a workaround and may corrupt other data. Create a backup of your save first!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Permanently delete items selected in options.\n\nPlease back up your save first!\nContinue?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Refresh Counts" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Load a saved city first to get numbers.>\n" +
                  "Updates all entity counts to show current city statistics.\n" +
                  "After cleaning, let the game run unpaused for a minute." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Status" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Shows the cleanup status. Updates live during an active cleanup; otherwise press [Refresh Counts] to recompute.\n\n" +
                  "\"**Idle**\" = no cleanup running or no city loaded yet.\n" +
                  "\"**Nothing to clean**\" = no citizens match the selected filters (or you already removed them).\n" +
                  "\"**Complete**\" = last cleanup finished; persists until you change filters or run a new cleanup." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Total Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Total number of citizen entities **currently in the simulation.**\n\n" +
                  "This number can differ from your population because it may include corrupt entities." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Citizens to Clean: select [ ✓ ] above" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Citizen entities that [Cleanup Citizens] will remove, based on the selected boxes [ ✓ ]." },

                // Read-only status rows
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)), "Citizen Count Comparison" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)),
                  "CC counts all non-deleted HouseholdMember entities. The game counts only valid moved-in citizens, so the totals are not expected to match." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)), "Personal Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)),
                  "Non-deleted PersonalCar entities, excluding temporary, destroyed, out-of-control, bicycle, and trailer entities." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)), "Personal-Car Parking" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)),
                  "Exclusive parked buckets. Street uses a visible ParkingLane; facility follows the parked lane's owner chain to a GarageLane, ParkingFacility, CarParkingFacility, or Building; OC hidden uses Unspawned plus an OC parked lane or TripSource." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)), "OC-Hidden Car Owners" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)),
                  "Separates a household located at an OC from the unusual case where the Owner is directly an OC entity. " +
                  "These are diagnostics, not automatic deletion candidates." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)), "OC-Hidden Staging Evidence" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)),
                  "Shows whether an OC-hidden car is linked by its parked lane or TripSource. " +
                  "TripSource at OC with no parked lane matches the game's initial no-nearby-parking fallback and is not proof of abandonment." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleStatusDisplay)), "Bicycles" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleStatusDisplay)),
                  "Bicycles are PersonalCar entities in the game, but are displayed separately because bicycle parking uses different infrastructure." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleParkingDisplay)), "Bicycle Parking" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleParkingDisplay)),
                  "Separates visible parked bicycles from hidden bicycles at outside connections or elsewhere." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)), "Potential Orphans" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)),
                  "Point-in-time ownership mismatches using the same rules as the game's PersonalCarOwnerSystem. " +
                  "The game normally removes these, so a small temporary count is possible." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)), "Where Potential Orphans Are Parked" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)),
                  "Shows where parked cars with an ownership mismatch were found. " +
                  "This is useful test data for a future cleanup feature; nothing is deleted." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)), "Updated" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)),
                  "Vehicle data is scanned once when the Options page first reads it, when you press [Refresh Counts], or when you write the debug report. There is no per-frame status scan." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Click [Refresh Counts]" },
                { "CitizenCleaner/Prompt/NoCity", "No city loaded" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Cleanup in progress… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Cleaning… {0}" },
                { "CitizenCleaner/Status/CitizenCountRow",
                  "CC household-member entities {0} | game valid moved-in citizens {1} | difference {2}" },
                { "CitizenCleaner/Status/CitizenCountPendingRow",
                  "CC household-member entities {0} | game counts are still initializing" },
                { "CitizenCleaner/Status/CarSummaryRow",
                  "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/CarParkingRow",
                  "Street {0} | building/parking facility {1} (hidden {2}) | OC hidden {3} | other {4} (hidden {5})" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow",
                  "City household {0} | household at OC {1} | direct OC owner {2} | nonresident/moving {3} | missing/non-household {4} | ownership mismatch {5}" },
                { "CitizenCleaner/Status/OcHiddenStageRow",
                  "OC evidence: parked lane {0} | TripSource {1} | TripSource with no lane {2} | HomeTarget {3} | keeper at OC {4}" },
                { "CitizenCleaner/Status/BicycleSummaryRow",
                  "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/BicycleParkingRow",
                  "Visible parked {0} | OC hidden {1} | hidden elsewhere {2}" },
                { "CitizenCleaner/Status/OwnershipRow",
                  "Cars {0}: no Owner {1} | owner has no buffer {2} | backlink missing {3} | bicycles {4}" },
                { "CitizenCleaner/Status/OwnershipLocationRow",
                  "Parked mismatches: street {0} | building/parking facility {1} | OC hidden {2} | other {3}" },
                { "CitizenCleaner/Status/CapturedAtRow", "Snapshot time {0}" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Mod Name" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Display name of this mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Current mod version." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Informational Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Mod Version with Commit ID" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "GitHub repository for the mod; opens in browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord chat for feedback on the mod; opens in browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox Mods website; opens in browser." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "USAGE" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Backup your save file first!>\n" +
                  "2. <Review the statistics; press [Refresh Counts] to update them.>\n" +
                  "3. [ ✓ ] <Select the items to include using the checkboxes>\n" +
                  "4. <Click [Cleanup Citizens] to clean up entities.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Notes:\n" +
                  "• This mod does **not** run automatically; use **[Cleanup Citizens]** each time for removals.\n" +
                  "• Revert to original saved city if needed for unexpected behavior." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug Tab — one read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Write Report to Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "- Writes report with" +
                  "**25 corrupt IDs**, **10 moving-away, 10 commuter, and 10 homeless IDs** (Index:Version).\n\n" +
                  "- Also includes the game/CC citizen-count comparison and personal-car/bicycle status.\n\n" +
                  "- **Read-only** — does not delete anything.\n\n" +
                  "- Log file at:\n" +
                  "%USERPROFILE%/AppData/LocalLow/Colossal Order/Cities Skylines II/logs/CitizenCleaner.log" },

                // Sentence UNDER the button (multiline text row)
                // LabelLocale = inline body under the button
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "One button writes the complete, troubleshooting report. Nothing is deleted." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Open Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                    "Open the **Logs/CitizenCleaner.log** file in the default text editor."
                },

            };
        }
        public void Unload() { }
    }
}
