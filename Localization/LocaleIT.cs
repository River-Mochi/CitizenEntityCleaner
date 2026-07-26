// LocaleIT.cs
using System.Collections.Generic;  // Dictionary
using Colossal;                    // IDictionarySource

namespace CitizenCleaner
{
    /// <summary>
    /// Italian locale entries (it-IT)
    /// </summary>
    public class LocaleIT : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleIT(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Azioni" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "Info" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Debug" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Gruppi da rimuovere" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Azioni" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "Cittadini corrotti" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Se abilitato (predefinito), conta e ripulisce i **Cittadini Corrotti**;\n" +
                  "residenti senza il componente PropertyRenter (e che non siano senzatetto, pendolari, turisti o in partenza).\n\n" +
                  "I cittadini corrotti sono l’obiettivo principale di questa mod. Se la città ne contiene troppi, nel tempo possono causare problemi." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "In partenza (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Se abilitato, conta e rimuove i cittadini con stato **In partenza** e Rent = 0 (cioè senza componente PropertyRenter).\n\n" +
                  "I cittadini in partenza con PropertyRenter o con Rent > 0 non vengono rimossi." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "Pendolari" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Se abilitato, conta e ripulisce i **Pendolari**. I pendolari non vivono nella tua città ma viaggiano per lavorare.\n\n" +
                  "A volte i pendolari vivevano qui in passato e sono andati via per mancanza di alloggio (funzione introdotta con la versione di gioco 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "Senzatetto" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Se abilitato, conta e ripulisce i **Senzatetto**.\n\n" +
                  "<ATTENZIONE>: eliminare i senzatetto può causare effetti imprevisti." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Pulisci cittadini" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "<Carica prima una città salvata.>\nRimuove i cittadini dai nuclei familiari che non hanno più il componente PropertyRenter.\n" +
                  "La pulizia include anche eventuali elementi opzionali selezionati [ ✓ ].\n\n" +
                  "**ATTENZIONE**: questa è una soluzione di ripiego e può danneggiare altri dati. Crea prima un backup del tuo salvataggio!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Elimina in modo permanente gli elementi selezionati nelle opzioni.\n\n<Per favore, esegui prima un backup!>\nContinuare?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Aggiorna conteggi" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Carica prima una città salvata per ottenere i numeri.>\n" +
                  "Aggiorna tutti i conteggi per mostrare le statistiche correnti della città.\n" +
                  "Dopo la pulizia, lascia il gioco in esecuzione per un minuto senza pausa." },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Scrivi rapporto diagnostico" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Scrive un rapporto leggibile: 25 ID corrotte e 10 ID per cittadini in trasferimento, pendolari e senzatetto; più conteggi e stato dei veicoli.\n\n" +
                  "**Sola lettura** — non elimina nulla." },

                // Sentence UNDER the button (multiline)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Un pulsante scrive il rapporto diagnostico completo. Non elimina nulla." },

                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Stato" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Mostra lo stato della pulizia. Si aggiorna in tempo reale durante una pulizia attiva; altrimenti premi [Aggiorna conteggi] per ricalcolare.\n\n" +
                  "\"**Idle**\" = nessuna pulizia in corso o nessuna città ancora caricata.\n" +
                  "\"**Nothing to clean**\" = nessun cittadino corrisponde ai filtri selezionati (oppure li hai già rimossi).\n" +
                  "\"**Complete**\" = l’ultima pulizia è terminata; persiste finché non cambi i filtri o avvii una nuova pulizia." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Cittadini totali" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Numero totale di entità cittadino **attualmente nella simulazione.**\n\n" +
                  "Questo numero può differire dalla popolazione perché può includere entità corrotte." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Cittadini da pulire: seleziona [ ✓ ] sopra" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Numero di entità cittadino che verranno rimosse quando fai clic su **[Pulisci cittadini]**,\n\n" +
                  "in base alle caselle selezionate [ ✓ ]." },

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
                { "CitizenCleaner/Prompt/RefreshCounts", "Fai clic su [Aggiorna conteggi]" },
                { "CitizenCleaner/Prompt/NoCity", "Nessuna città caricata" },
                { "CitizenCleaner/Prompt/Error",  "Errore" },
                { "CitizenCleaner/Status/Progress", "Pulizia in corso… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Pulizia… {0}" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nome mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Nome visualizzato di questa mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Versione" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Versione corrente della mod." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Versione informativa" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Versione della mod con ID commit" },
#endif

                // About tab links (external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Repository GitHub della mod; si apre nel browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Canale Discord per feedback sulla mod; si apre nel browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Sito Paradox Mods; si apre nel browser." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "UTILIZZO" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Esegui prima il backup del salvataggio!>\n" +
                  "2. <Fai clic su [Aggiorna conteggi] per vedere le statistiche correnti.>\n" +
                  "3. [ ✓ ] <Seleziona gli elementi da includere usando le caselle>\n" +
                  "4. <Fai clic su [Pulisci cittadini] per ripulire le entità.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Note:\n" +
                  "• Questa mod **non** funziona automaticamente; usa **[Pulisci cittadini]** ogni volta che vuoi rimuovere elementi.\n" +
                  "• In caso di comportamenti imprevisti, torna al salvataggio originale." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}
