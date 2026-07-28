// Localization/LocaleEN.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATUS" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Corrupt Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "When enabled (default), marks/counts **Corrupt** citizens.\n" +
                  "These are citizen without PropertyRenter and are not homeless, commuters, tourists, or moving-away.\n\n" +
                  "- Abandoned Cars: corrupt citizens and their abandoned cars are the main target of this mod.\n" +
                  "- When citizens are cleaned up, the game should remove associated personal vehicles and free parking spaces.\n" +
                  "- CC marks citizens for deletion; the game's cleanup systems handles remaining references (school, patients, etc.)." },

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

                // Compact vehicle status
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)), "Personal Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)),
                  "Personal cars only; bicycles and trailers are excluded." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)), "Parked Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)),
                  "Parked on streets, in parking facilities, or hidden at Outside Connections (OC)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)), "Hidden at OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)),
                  "OC-hidden cars grouped by owner. See [Write Report] for full details." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)), "Possible Orphans" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)),
                  "Cars with an ownership mismatch. A temporary count is possible while the game updates." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "Log Report" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Write full citizen and vehicle details to **Logs/CitizenCleaner.log**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Open Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)),
                  "Open **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Click [Refresh Counts]" },
                { "CitizenCleaner/Prompt/NoCity", "No city loaded" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Cleanup in progress… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Cleaning… {0}" },
                { "CitizenCleaner/Status/CarSummaryRow",
                  "{0} active | {1} parked | {2} total" },
                { "CitizenCleaner/Status/CarParkingRow",
                  "{0} street | {1} facility | {2} OC hidden | {3} other" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow",
                  "{0} city | {1} OC | {2} away | {3} missing | updated {4}" },
                { "CitizenCleaner/Status/OwnershipRow",
                  "{0} total | {1} street | {2} facility | {3} at OC" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — DIAGNOSTIC REPORT\nGenerated: {0}" },
                { "CitizenCleaner/Report/CitizenCounts",
                  "[CITIZEN COUNTS]\n" +
                  "CC household-member entities : {0}\n" +
                  "Game valid moved-in citizens  : {1}\n" +
                  "Difference (CC - game)         : {2}\n" +
                  "Game homeless citizens        : {3}\n" +
                  "Game moving-away households   : {4}\n" +
                  "Game commuter households      : {5}\n" +
                  "Game tourist citizens         : {6}\n" +
                  "CC includes every non-deleted HouseholdMember; the game total includes valid moved-in citizens only." },
                { "CitizenCleaner/Report/GameCountsPending",
                  "Game counts are still initializing." },
                { "CitizenCleaner/Report/CitizenIdsHeading",
                  "[CITIZEN ENTITY IDs — use Scene Explorer; Index:Version]" },
                { "CitizenCleaner/Report/CorruptCitizens",
                  "Corrupt citizens" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Moving-away citizens (household MovingAway + no PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens",
                  "Commuter citizens" },
                { "CitizenCleaner/Report/HomelessCitizens",
                  "Homeless citizens" },
                { "CitizenCleaner/Report/EntitySampleSummary",
                  "{0}: {1} total | {2} IDs" },
                { "CitizenCleaner/Report/IdsLabel", "IDs: " },
                { "CitizenCleaner/Report/None", "(none)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[PERSONAL VEHICLES]\nVehicle snapshot unavailable.\n" },
                { "CitizenCleaner/Report/PersonalCars",
                  "[PERSONAL CARS — bicycles excluded]\n" +
                  "{0} active | {1} parked | {2} total | {3} other\n" +
                  "Parked: {4} street | {5} facility ({6} hidden) | {7} OC hidden | {8} other ({9} hidden)" },
                { "CitizenCleaner/Report/PossibleOrphans",
                  "[POSSIBLE ORPHANS]\n" +
                  "{0} total | {1} missing Owner | {2} missing OwnedVehicle buffer | {3} missing backlink\n" +
                  "Parked: {4} street | {5} facility | {6} OC hidden | {7} other" },
                { "CitizenCleaner/Report/OcHiddenCars",
                  "[OC-HIDDEN CARS]\n" +
                  "Owners: {0} city household | {1} household at OC | {2} direct OC entity | " +
                  "{3} nonresident/moving | {4} missing/non-household\n" +
                  "Evidence: {5} parked lane at OC | {6} TripSource at OC | {7} TripSource at OC without lane\n" +
                  "Trip state: {8} HomeTarget | {9} keeper at OC" },
                { "CitizenCleaner/Report/Bicycles",
                  "[BICYCLES]\n" +
                  "{0} active | {1} parked | {2} total | {3} other\n" +
                  "Parked: {4} visible | {5} OC hidden | {6} hidden elsewhere\n" +
                  "Keeper mismatches: {7}" },
                { "CitizenCleaner/Report/Definitions",
                  "[DEFINITIONS]\n" +
                  "Street: visible ParkedCar on ParkingLane, excluding facilities.\n" +
                  "Facility: parked lane owned by a garage, parking facility, or building.\n" +
                  "OC hidden: ParkedCar + Unspawned linked to an outside connection.\n" +
                  "Ownership mismatches can be temporary; location alone does not prove abandonment." },


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
                 { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox Mods website; opens in browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "GitHub link for feedback on the mod." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord chat for feedback on the mod; opens in browser." },

               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Log Entity IDs" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Writes Entity IDs (Index:Version) for samples of **25 corrupt**, **10 moving-away, 10 commuter, and 10 homeless citizens**.\n" +
                  "Use the **Scene Explorer** mod to inspect an Entity ID.\n" +
                  "Also includes full citizen and personal-vehicle statistics.\n" +
                  "Notepad++ or similar app is good to view log files."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Use [Log Entity IDs], [Open Log], then copy an Entity ID into Scene Explorer mod inside the city." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Open Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Opens **Logs/CitizenCleaner.log** or the Logs folder if the file is not available." },

            };
        }
        public void Unload() { }
    }
}
