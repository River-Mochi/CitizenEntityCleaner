// Localization/LocaleDE.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

    /// <summary>
    /// German locale (de-DE)
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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATUS" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Korrupte Bürger" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Wenn aktiviert (Standard), werden **korrupte** Bürger gezählt.\n" +
                  "Diese Bürger gehören zu Haushalten ohne PropertyRenter und sind weder obdachlos noch Pendler, Touristen oder wegziehend.\n\n" +
                  "- **Verlassene Autos:** Korrupte Bürger und verlassene Autos sind das Hauptziel.\n" +
                  "- Wenn keine Haushaltsmitglieder übrig sind, sollte das Spiel das persönliche Fahrzeug entfernen und den Parkplatz freigeben.\n" +
                  "- CC markiert Bürger zum Löschen; die Bereinigungssysteme des Spiels verarbeiten Fahrzeug-, Schul-, Patienten- und andere Verweise." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ Wegziehende (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Wenn aktiviert, zählt und entfernt Bürger mit dem Status **Wegziehend** und Rent = 0 (also ohne PropertyRenter-Komponente).\n\n" +
                  "Wegziehende mit PropertyRenter oder Rent > 0 werden nicht entfernt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Pendler" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Wenn aktiviert, zählt und entfernt **Pendler**. Pendler wohnen nicht in deiner Stadt, kommen aber zur Arbeit hierher.\n\n" +
                  "Manche lebten früher hier und sind wegen Obdachlosigkeit weggezogen (seit Spielversion 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Obdachlose" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Zählt und bereinigt lebende Bürger mit **ValidCitizen + Homeless**.\n" +
                  "Tote, Touristen, Pendler und Bürger ohne ValidCitizen werden ausgeschlossen.\n\n" +
                  "<VORSICHT>: Das Löschen Obdachloser kann unbekannte Nebenwirkungen verursachen." },

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

                // Cleanup Status and Counts
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

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Autos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Nur persönliche Autos; Fahrzeuge der Fahrradgruppe und Anhänger werden separat gemeldet.\n" +
                  "<Aktiv> = auf einer Fahrspur und nicht geparkt; kann fahren oder stehen.\n" +
                  "<Geparkt> = alle geparkten persönlichen Autos.\n" +
                  "<Gesamt> = aktive, geparkte und im Übergang befindliche persönliche Autos.\n" +
                  "<Aktualisiert> = Zeitpunkt der letzten Aktualisierung.\n\n" +
                  "In den Optionen ist die Stadtsimulation pausiert. Lasse die Stadt laufen, bevor du aktualisierst." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Geparkte Autos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Straße> = sichtbare, auf einer ParkingLane am Straßenrand geparkte Autos.\n" +
                  "<Anlage> = Autos in einem Gebäude, einer Garage oder Parkanlage.\n" +
                  "<OC> = versteckte Autos an einer Außenverbindung.\n" +
                  "<Sonstige> = sonstige geparkte Autos; einige haben keine zugewiesene Parkspur.\n" +
                  "<Keine Spur> allein bedeutet nicht, dass ein Auto verlassen ist.\n\n" +
                  "Nutze **[LOG-BERICHT]** und dann **[LOG ÖFFNEN]** für Details und Entity-IDs." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC-Autos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Versteckte Autos an einer Außenverbindung, nach Besitzer gruppiert.\n" +
                  "<Stadt> = Besitzer ist ein Stadthaushalt.\n" +
                  "<Am OC> = Besitzerhaushalt befindet sich derzeit an einem OC.\n" +
                  "<OC-Besitzer> = normalerweise vom Spiel erzeugter DummyTraffic, kein Bewohnerauto.\n" +
                  "<Auswärts> = Pendler-, Touristen- oder wegziehender Haushalt.\n" +
                  "<Fehlend> = kein Besitzer oder Besitzer ist kein Haushalt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "LOG-BERICHT" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Schreibt Bürger- und Fahrzeugzahlen sowie Beispiel-**Entity-IDs** in CitizenCleaner.log.\n" +
                  "Kopiere eine ID in den Mod **Scene Explorer**, um sie zu prüfen." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "LOG ÖFFNEN" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Öffnet **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Klicke auf [Aktualisieren]" },
                { "CitizenCleaner/Prompt/NoCity", "Keine Stadt geladen" },
                { "CitizenCleaner/Prompt/Error",  "Fehler" },
                { "CitizenCleaner/Status/Progress", "Bereinigung läuft… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Bereinige… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} aktiv | {1} geparkt | {2} gesamt | aktualisiert {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} Straße | {1} Anlage | {2} OC | {3} sonstige" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} Stadt | {1} am OC | {2} OC-Besitzer | {3} auswärts | {4} fehlend" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — LOG-BERICHT\n" +
                  "Erstellt: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[BÜRGERZAHL-GEGENPRÜFUNG — SPIEL 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Die Zähler aus Spiel 1.6 dienen nur zur Diagnose; CC verwendet eine eigene Bereinigungszahl.\n" +
                  "ValidCitizen ist ein Bevölkerungsmerkmal für eingezogene Bürger, nicht der Test für korrupte Bürger von CC.\n" +
                  "Das Spiel zählt wegziehende und Pendler-Haushalte; CC zählt Bürger." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[PRÜFUNG DER OBDACHLOSEN-BERECHTIGUNG]" },
                { "CitizenCleaner/Report/GameCountsPending", "Die Spielzähler werden noch initialisiert." },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[BÜRGER-ENTITY-IDs — mit Scene Explorer prüfen; Index:Version]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Korrupte Bürger" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "Wegziehende Bürger (Haushalt MovingAway + kein PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Pendler" },
                { "CitizenCleaner/Report/HomelessCitizens", "Berechtigte obdachlose Bürger" },
                { "CitizenCleaner/Report/IdsLabel", "IDs: " },
                { "CitizenCleaner/Report/None", "(keine)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[PERSÖNLICHE FAHRZEUGE]\n" +
                  "Fahrzeug-Snapshot nicht verfügbar.\n" },


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

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox-Mods-Website; öffnet im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "GitHub-Repository des Mods; öffnet im Browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord-Kanal für Feedback; öffnet im Browser." },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Entity-IDs protokollieren" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Protokolliert Beispiele für **25 korrupte**, **10 wegziehende, 10 Pendler und 10 obdachlose Bürger**.\n" +
                  "Protokolliert außerdem verdächtige Fahrzeug-Entity-IDs.\n" +
                  "Prüfe eine ID mit dem Mod **Scene Explorer**." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Nutze [Entity-IDs protokollieren], [Log öffnen] und kopiere dann in der Stadt eine Entity-ID in den Mod Scene Explorer." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Log öffnen" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Öffnet **Logs/CitizenCleaner.log** oder den Logs-Ordner, falls die Datei nicht verfügbar ist." },

            };
        }
        public void Unload() { }
    }
}
