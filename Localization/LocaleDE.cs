// LocaleDE.cs
using System.Collections.Generic;  // Dictionary
using Colossal;                    // IDictionarySource

namespace CitizenCleaner
{
    /// <summary>
    /// German locale entries (de-DE)
    /// </summary>
    public class LocaleDE : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleDE(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Aktionen" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "Über" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Debug" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Aufräumziele" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Aktionen" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "Korrupte Bürger" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Wenn aktiviert (Standard), zählt und entfernt **korrupte Bürger**:\n" +
                  "Bürger ohne PropertyRenter-Komponente (also weder Obdachlose, Pendler, Touristen noch Wegziehende).\n\n" +
                  "Korrupte Bürger sind das Hauptziel dieses Mods. Zu viele können langfristig Probleme verursachen." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "Wegziehende (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Wenn aktiviert, zählt und entfernt Bürger mit dem Status **Wegziehend** und Rent = 0 (also ohne PropertyRenter-Komponente).\n\n" +
                  "Wegziehende mit PropertyRenter oder Rent > 0 werden nicht entfernt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "Pendler" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Wenn aktiviert, zählt und entfernt **Pendler**. Pendler wohnen nicht in deiner Stadt, kommen aber zur Arbeit hierher.\n\n" +
                  "Manche lebten früher hier und sind wegen Obdachlosigkeit weggezogen (seit Spielversion 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "Obdachlose" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Wenn aktiviert, zählt und entfernt **Obdachlose**.\n\n" +
                  "**VORSICHT:** Das Entfernen von Obdachlosen kann zu unerwarteten Nebenwirkungen führen." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Bürger bereinigen" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "<Zuerst einen Spielstand laden.>\nEntfernt Bürger aus Haushalten, die keine PropertyRenter-Komponente mehr haben.\n" +
                  "Die Bereinigung umfasst auch alle optional markierten Elemente [✓].\n\n" +
                  "**VORSICHT:** Dies ist ein Workaround und kann andere Daten beschädigen. Erstelle zuerst ein Backup deines Spielstands!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Ausgewählte Elemente in den Optionen werden dauerhaft gelöscht.\n\n<Bitte zuerst ein Backup erstellen!>\nFortfahren?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Aktualisieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Lade zuerst einen Spielstand, um Zahlen zu erhalten.>\n" +
                  "Aktualisiert alle Zähler, um die aktuellen Stadtstatistiken anzuzeigen.\n" +
                  "Lasse das Spiel nach dem Bereinigen eine Minute lang unpausiert laufen." },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Diagnosebericht ins Log schreiben" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Schreibt einen lesbaren Bericht: 25 korrupte IDs sowie je 10 IDs für Wegziehende, Pendler und Obdachlose; außerdem Bürger- und Fahrzeugstatus.\n\n" +
                  "**Nur Lesen** — es wird nichts gelöscht." },


                // Sentence UNDER the button (multiline)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Eine Schaltfläche schreibt den vollständigen Fehlerbericht. Es wird nichts gelöscht." },

                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Status" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Zeigt den Bereinigungsstatus. Aktualisiert sich live während einer laufenden Bereinigung; ansonsten [Aktualisieren] drücken, um neu zu berechnen.\n\n" +
                  "\"**Idle**\" = keine Bereinigung läuft oder noch keine Stadt geladen.\n" +
                  "\"**Nothing to clean**\" = keine Bürger entsprechen den gewählten Filtern (oder du hast sie bereits entfernt).\n" +
                  "\"**Complete**\" = letzte Bereinigung abgeschlossen; bleibt bestehen, bis du Filter änderst oder erneut bereinigst." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Bürger gesamt" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Gesamtzahl der Bürger-Entitäten **derzeit in der Simulation.**\n\n" +
                  "Kann von der Bevölkerung abweichen, da möglicherweise korrupte Entitäten enthalten sind." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Zu bereinigende Bürger: oben [ ✓ ] wählen" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Anzahl der Bürger-Entitäten, die beim Klick auf **[Bürger bereinigen]** entfernt werden,\n\n" +
                  "abhängig von den gewählten Kästchen [ ✓ ]." },

                // New status rows (English fallback until this locale is translated)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)), "Citizen Count Comparison" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)), "Personal Cars" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)), "Personal-Car Parking" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)), "OC-Hidden Car Owners" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)), "OC-Hidden Staging Evidence" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleStatusDisplay)), "Bicycles" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleParkingDisplay)), "Bicycle Parking" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)), "Potential Orphans" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)), "Where Potential Orphans Are Parked" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)), "Updated" },
                { "CitizenCleaner/Status/CitizenCountRow", "CC household-member entities {0} | game valid moved-in citizens {1} | difference {2}" },
                { "CitizenCleaner/Status/CitizenCountPendingRow", "CC household-member entities {0} | game counts are still initializing" },
                { "CitizenCleaner/Status/CarSummaryRow", "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/CarParkingRow", "Street {0} | building/parking facility {1} (hidden {2}) | OC hidden {3} | other {4} (hidden {5})" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow", "City household {0} | household at OC {1} | direct OC owner {2} | nonresident/moving {3} | missing/non-household {4} | ownership mismatch {5}" },
                { "CitizenCleaner/Status/OcHiddenStageRow", "OC evidence: parked lane {0} | TripSource {1} | TripSource with no lane {2} | HomeTarget {3} | keeper at OC {4}" },
                { "CitizenCleaner/Status/BicycleSummaryRow", "Total {0} | active {1} | parked {2} | transitioning/other {3}" },
                { "CitizenCleaner/Status/BicycleParkingRow", "Visible parked {0} | OC hidden {1} | hidden elsewhere {2}" },
                { "CitizenCleaner/Status/OwnershipRow", "Cars {0}: no Owner {1} | owner has no buffer {2} | backlink missing {3} | bicycles {4}" },
                { "CitizenCleaner/Status/OwnershipLocationRow", "Parked mismatches: street {0} | building/parking facility {1} | OC hidden {2} | other {3}" },
                { "CitizenCleaner/Status/CapturedAtRow", "Snapshot time {0}" },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Klicke auf [Aktualisieren]" },
                { "CitizenCleaner/Prompt/NoCity", "Keine Stadt geladen" },
                { "CitizenCleaner/Prompt/Error",  "Fehler" },
                { "CitizenCleaner/Status/Progress", "Bereinigung läuft… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Bereinige… {0}" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Mod-Name" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Anzeigename dieses Mods." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Aktuelle Mod-Version." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Info-Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Mod-Version mit Commit-ID" },
#endif

                // About tab links
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "GitHub-Repository des Mods; öffnet im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord-Kanal für Feedback; öffnet im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox-Mods-Website; öffnet im Browser." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "NUTZUNG" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Sichere zuerst deinen Spielstand!>\n" +
                  "2. <Klicke [Aktualisieren], um die aktuellen Statistiken zu sehen.>\n" +
                  "3. [ ✓ ] <Wähle die gewünschten Elemente über die Kästchen>\n" +
                  "4. <Klicke [Bürger bereinigen], um die Bereinigung zu starten.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Hinweise:\n" +
                  "• Dieser Mod läuft **nicht** automatisch; verwende **[Bürger bereinigen]** bei Bedarf.\n" +
                  "• Bei unerwartetem Verhalten zur ursprünglichen Speicherung zurückkehren." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}
