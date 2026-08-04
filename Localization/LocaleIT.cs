// Localization/LocaleIT.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Italian locale (it-IT)
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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATO" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debug" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Cittadini corrotti" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Quando è attivo (impostazione predefinita), conta i cittadini **corrotti**.\n" +
                  "Appartengono a famiglie senza PropertyRenter e non sono senzatetto, pendolari, turisti o in partenza.\n\n" +
                  "- **Auto abbandonate:** i cittadini corrotti e le auto abbandonate sono l'obiettivo principale.\n" +
                  "- Quando non resta alcun membro della famiglia, il gioco dovrebbe rimuovere il veicolo personale e liberare il parcheggio.\n" +
                  "- CC contrassegna i cittadini per l'eliminazione; i sistemi del gioco gestiscono i riferimenti a veicoli, scuole, pazienti e altro." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ In partenza (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Se abilitato, conta e rimuove i cittadini con stato **In partenza** e Rent = 0 (cioè senza componente PropertyRenter).\n\n" +
                  "I cittadini in partenza con PropertyRenter o con Rent > 0 non vengono rimossi." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Pendolari" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Se abilitato, conta e ripulisce i **Pendolari**. I pendolari non vivono nella tua città ma viaggiano per lavorare.\n\n" +
                  "A volte i pendolari vivevano qui in passato e sono andati via per mancanza di alloggio (funzione introdotta con la versione di gioco 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Senzatetto" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Conta e ripulisce i membri di **HomelessHousehold**.\n\n" +
                  "Eliminare i senzatetto cambia la popolazione e la domanda residenziale.\n" +
                  "Più famiglie senzatetto riducono la domanda generale, ma aumentano il fattore positivo per l'alta densità." },

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

                // Cleanup Status and Counts
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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "Abitazioni" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "Conteggi attuali delle famiglie; aggiornati con [Aggiorna conteggi].\n" +
                  "<In cerca> = famiglie con PropertySeeker attivo, incluse le famiglie senzatetto.\n" +
                  "<In arrivo/uscita> = contatori delle famiglie del gioco 1.6.\n" +
                  "PropertySeeker indica una ricerca in corso, non un fallimento." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Auto" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Solo auto personali; i veicoli del gruppo biciclette e i rimorchi sono indicati separatamente.\n" +
                  "<Attive> = su una corsia e non parcheggiate; possono muoversi o essere ferme.\n" +
                  "<Parcheggiate> = tutte le auto personali parcheggiate.\n" +
                  "<Totale> = auto personali attive, parcheggiate e in transizione.\n" +
                  "<Aggiornato> = ora di aggiornamento dei conteggi.\n\n" +
                  "La simulazione è in pausa nelle Opzioni. Avvia la città prima di aggiornare." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Auto parcheggiate" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Strada> = auto visibili parcheggiate su una ParkingLane stradale.\n" +
                  "<Struttura> = auto in un edificio, garage o struttura di parcheggio.\n" +
                  "<OC> = auto nascoste presso una connessione esterna.\n" +
                  "<Altro> = auto parcheggiate non classificate sopra; alcune non hanno una corsia di parcheggio assegnata.\n" +
                  "<Nessuna corsia> da solo non significa che l'auto sia abbandonata.\n\n" +
                  "Usa **[RAPPORTO LOG]**, poi **[APRI LOG]**, per dettagli e ID delle entità." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Auto presso OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Auto nascoste presso una connessione esterna, raggruppate per proprietario.\n" +
                  "<Città> = il proprietario è una famiglia della città.\n" +
                  "<Presso OC> = la famiglia proprietaria si trova attualmente presso una OC.\n" +
                  "<Proprietario OC> = normalmente DummyTraffic creato dal gioco, non un'auto residente.\n" +
                  "<Fuori> = famiglia pendolare, turista o in partenza.\n" +
                  "<Mancante> = nessun proprietario o il proprietario non è una famiglia." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "RAPPORTO LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Scrive i conteggi di cittadini e veicoli, più esempi di **ID entità**, in CitizenCleaner.log.\n" +
                  "Copia un ID nel mod **Scene Explorer** per esaminarlo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "APRI LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Apre **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Fai clic su [Aggiorna conteggi]" },
                { "CitizenCleaner/Prompt/NoCity", "Nessuna città caricata" },
                { "CitizenCleaner/Prompt/Error",  "Errore" },
                { "CitizenCleaner/Status/Progress", "Pulizia in corso… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Pulizia… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} in cerca | {1} in arrivo | {2} in uscita" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} attive | {1} parcheggiate | {2} totale | aggiornato {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} strada | {1} struttura | {2} OC | {3} altro" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} città | {1} presso OC | {2} proprietario OC | {3} fuori | {4} mancante" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — RAPPORTO LOG\n" +
                  "Generato: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[CONTROLLO INCROCIATO CITTADINI — GIOCO 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "I contatori del gioco 1.6 sono solo diagnostici; CC usa il proprio conteggio di pulizia.\n" +
                  "ValidCitizen è un indicatore della popolazione trasferita in città, non il test di CC per i cittadini corrotti.\n" +
                  "Il gioco conta le famiglie in partenza e pendolari; CC conta i cittadini." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[CONFRONTO POPOLAZIONE SENZATETTO]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[STATI ABITATIVI DELLE FAMIGLIE]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "Questi stati possono sovrapporsi. PropertySeeker significa ricerca, non fallimento. L'attuale regola di CC per i cittadini corrotti non li esclude." },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "Famiglie senza PropertyRenter e non MovedIn" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "Famiglie senza PropertyRenter con PropertySeeker abilitato" },
                { "CitizenCleaner/Report/GameCountsPending", "I conteggi del gioco sono ancora in inizializzazione." },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[ID ENTITÀ CITTADINI — usa Scene Explorer; Indice:Versione]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Cittadini corrotti" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Cittadini in partenza (famiglia MovingAway + senza PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Cittadini pendolari" },
                { "CitizenCleaner/Report/HomelessCitizens", "Senzatetto candidati alla pulizia" },
                { "CitizenCleaner/Report/IdsLabel", "ID: " },
                { "CitizenCleaner/Report/None", "(nessuno)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[VEICOLI PERSONALI]\n" +
                  "Snapshot dei veicoli non disponibile.\n" },


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

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Sito Paradox Mods; si apre nel browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Repository GitHub della mod; si apre nel browser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Canale Discord per feedback sulla mod; si apre nel browser." },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Registra ID entità" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Registra esempi di **25 cittadini corrotti**, **10 in partenza, 10 pendolari e 10 senzatetto**.\n" +
                  "Registra anche gli ID entità dei veicoli sospetti.\n" +
                  "Usa il mod **Scene Explorer** per esaminare un ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Usa [Registra ID entità], [Apri log], poi copia un ID entità nel mod Scene Explorer all'interno della città." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Apri log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Apre **Logs/CitizenCleaner.log** o la cartella Logs se il file non è disponibile." },

            };
        }
        public void Unload() { }
    }
}
