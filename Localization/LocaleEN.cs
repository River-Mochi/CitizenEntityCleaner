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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATUS" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Corrupt Citizens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "When enabled (default), counts **Corrupt** citizens.\n" +
                  "These citizens belong to households without PropertyRenter and are not homeless, commuters, tourists, or moving-away.\n\n" +
                  "- **Abandoned Cars:** corrupt citizens and abandoned cars are the main target.\n" +
                  "- When no household members remain, the game should remove its personal vehicle and free the parking space.\n" +
                  "- CC marks citizens for deletion; the game's cleanup systems handle vehicle, school, patient, and other references." },


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
                  "Counts and cleans alive citizens marked **ValidCitizen + Homeless**.\n" +
                  "Dead, tourist, commuter, and citizens missing ValidCitizen are excluded.\n\n" +
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

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                    "Personal cars only; bicycle-group vehicles and trailers are reported separately.\n" +
                    "<Active> = on a lane and not parked; it may be moving or stopped.\n" +
                    "<Parked> = all parked personal cars.\n" +
                    "<Total> = active, parked, and transitioning personal cars.\n" +
                    "<Updated> = time these counts were refreshed.\n\n" +
                    "The city simulation is paused in Options. Run the city before refreshing to see simulation changes."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Parked Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                    "<Street> = visible parked cars on a street ParkingLane.\n" +
                    "<Facility> = cars in a building, garage, or parking facility.\n" +
                    "<OC> = hidden cars at an Outside Connection.\n" +
                    "<Other> = parked cars not matched above; some have no assigned parking lane.\n" +
                    "<No lane> alone does not mean abandoned.\n\n" +
                    "Use **[LOG REPORT]**, then **[OPEN LOG]**, for more details and Entity IDs.\n" +
                    "Use Scene Explorer mod to research, and jump to Entity ID numbers."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC Cars" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                    "Hidden cars at an Outside Connection, grouped by owner.\n" +
                    "<City> = owner is a city household.\n" +
                    "<At OC> = owner household is currently at an OC.\n" +
                    "<OC owner> = normally game-created DummyTraffic, not a resident car.\n" +
                    "<Away> = commuter, tourist, or moving-away household.\n" +
                    "<Missing> = no owner or owner is not a household.\n" +
                    "Use **[LOG REPORT]**, then **[OPEN LOG]**, for more details and Entity IDs."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "LOG REPORT" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Writes citizen and vehicle counts plus sample **Entity IDs** to CitizenCleaner.log.\n" +
                  "Copy an ID into the **Scene Explorer** mod to inspect it." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "OPEN LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)),
                  "Open **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Click [Refresh Counts]" },
                { "CitizenCleaner/Prompt/NoCity", "No city loaded" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Cleanup in progress… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Cleaning… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2",
                  "{0} active | {1} parked | {2} total | updated {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2",
                  "{0} street | {1} facility | {2} OC | {3} other" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} city | {1} at OC | {2} OC owner | {3} away | {4} missing" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — LOG REPORT\nGenerated: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading",
                  "[CITIZEN COUNT CROSS-CHECK — GAME 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Game 1.6 counters are diagnostic only; CC uses its own cleanup count.\n" +
                  "ValidCitizen is a moved-in population flag, not CC's corrupt-citizen test.\n" +
                  "Game moving-away and commuter counts are households; CC counts citizens." },

                { "CitizenCleaner/Report/HomelessCheckHeading",
                  "[HOMELESS ELIGIBILITY CHECK]" },
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
                  "Eligible homeless citizens" },
                { "CitizenCleaner/Report/IdsLabel", "IDs: " },
                { "CitizenCleaner/Report/None", "(none)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[PERSONAL VEHICLES]\nVehicle snapshot unavailable.\n" },


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
                  "4. <Click [Cleanup Citizens] to clean up entities.>\n" +
                  "5. Status report is data only."
                },
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
                  "Logs samples of **25 corrupt**, **10 moving-away, 10 commuter, and 10 homeless citizens**.\n" +
                  "Also logs vehicle Entity IDs for research.\n" +
                  "Use the **Scene Explorer** mod to inspect an ID."
                },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Use [Log Entity IDs], [Open Log]. Then copy an Entity ID into Scene Explorer mod when inside the city." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Open Log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Opens **Logs/CitizenCleaner.log** or the Logs folder if the file is not available." },

            };
        }
        public void Unload() { }
    }
}
