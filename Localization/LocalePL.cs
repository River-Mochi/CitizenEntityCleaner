// Localization/LocalePL.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

    /// <summary>
    /// Polish locale (pl-PL)
    /// </summary>
    public class LocalePL : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocalePL(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Działania" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "Informacje" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Debugowanie" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Cele czyszczenia" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Działania" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATUS" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Informacje" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debugowanie" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Uszkodzeni mieszkańcy" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Po włączeniu (domyślnie) zlicza **uszkodzonych** mieszkańców.\n" +
                  "Należą do gospodarstw bez PropertyRenter i nie są bezdomni, dojeżdżający, turyści ani wyprowadzający się.\n\n" +
                  "- **Porzucone samochody:** uszkodzeni mieszkańcy i porzucone samochody są głównym celem.\n" +
                  "- Gdy nie zostanie żaden członek gospodarstwa, gra powinna usunąć jego pojazd osobisty i zwolnić miejsce parkingowe.\n" +
                  "- CC oznacza mieszkańców do usunięcia; systemy gry obsługują odwołania do pojazdów, szkół, pacjentów i inne." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "▪ Wyprowadzający się (czynsz = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Po włączeniu zlicza i usuwa mieszkańców **wyprowadzających się** z czynszem równym 0 (bez komponentu PropertyRenter).\n\n" +
                  "Osoby wyprowadzające się, które mają PropertyRenter lub czynsz większy od 0, nie są usuwane." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Dojeżdżający" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Po włączeniu zlicza i usuwa **dojeżdżających**. Nie mieszkają oni w mieście, lecz przyjeżdżają do niego do pracy.\n\n" +
                  "Czasami wcześniej mieszkali w mieście, ale wyprowadzili się z powodu bezdomności (funkcja dodana w wersji gry 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Bezdomni" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Zlicza i usuwa żyjących mieszkańców oznaczonych **ValidCitizen + Homeless**.\n" +
                  "Zmarli, turyści, dojeżdżający i mieszkańcy bez ValidCitizen są pomijani.\n\n" +
                  "<UWAGA>: usuwanie bezdomnych może powodować nieznane skutki uboczne." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Wyczyść mieszkańców" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Najpierw wczytaj zapisane miasto.\nUsuwa mieszkańców z gospodarstw domowych, które nie mają już komponentu PropertyRenter.\n" +
                  "Czyszczenie obejmuje także zaznaczone [ ✓ ] elementy opcjonalne.\n\n" +
                  "**UWAGA**: to obejście może uszkodzić inne dane. Najpierw utwórz kopię zapasową zapisu!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Trwale usunąć elementy zaznaczone w opcjach?\n\nNajpierw wykonaj kopię zapasową zapisu!\n Kontynuować?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Odśwież liczniki" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Najpierw wczytaj zapisane miasto, aby zobaczyć liczby.>\n" +
                  "Aktualizuje wszystkie liczniki encji i pokazuje bieżące statystyki miasta.\n" +
                  "Po czyszczeniu uruchom grę bez pauzy na minutę." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Stan" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Pokazuje stan czyszczenia. Podczas czyszczenia aktualizuje się na żywo; w innym przypadku naciśnij [Odśwież liczniki].\n\n" +
                  "\"**Bezczynny**\" = czyszczenie nie działa lub nie wczytano miasta.\n" +
                  "\"**Brak elementów do usunięcia**\" = żaden mieszkaniec nie pasuje do filtrów.\n" +
                  "\"**Ukończono**\" = ostatnie czyszczenie zakończono; stan pozostaje do zmiany filtrów lub nowego czyszczenia." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Łączna liczba mieszkańców" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Łączna liczba encji mieszkańców **obecnie w symulacji.**\n\n" +
                  "Może różnić się od populacji, ponieważ może obejmować uszkodzone encje." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Mieszkańcy do usunięcia: zaznacz [ ✓ ] powyżej" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Liczba encji mieszkańców, które zostaną usunięte po kliknięciu **[Wyczyść]**,\n\n" +
                  "zgodnie z zaznaczonymi polami [ ✓ ]." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Samochody" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Tylko samochody osobiste; pojazdy z grupy rowerów i przyczepy są raportowane osobno.\n" +
                  "<Aktywne> = są na pasie i nie są zaparkowane; mogą jechać lub stać.\n" +
                  "<Zaparkowane> = wszystkie zaparkowane samochody osobiste.\n" +
                  "<Razem> = aktywne, zaparkowane i przechodzące między stanami samochody osobiste.\n" +
                  "<Aktualizacja> = czas ostatniego odświeżenia danych.\n\n" +
                  "W Opcjach symulacja miasta jest wstrzymana. Uruchom miasto przed odświeżeniem." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Zaparkowane samochody" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Ulica> = widoczne samochody zaparkowane na ulicznym ParkingLane.\n" +
                  "<Obiekt> = samochody w budynku, garażu lub obiekcie parkingowym.\n" +
                  "<OC> = ukryte samochody przy połączeniu zewnętrznym.\n" +
                  "<Inne> = zaparkowane samochody niepasujące do powyższych; część nie ma przypisanego pasa parkingowego.\n" +
                  "<Brak pasa> sam w sobie nie oznacza porzuconego samochodu.\n\n" +
                  "Użyj **[RAPORT LOGU]**, a następnie **[OTWÓRZ LOG]**, aby zobaczyć szczegóły i identyfikatory encji." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Samochody przy OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Ukryte samochody przy połączeniu zewnętrznym, pogrupowane według właściciela.\n" +
                  "<Miasto> = właścicielem jest gospodarstwo z miasta.\n" +
                  "<Przy OC> = gospodarstwo właściciela znajduje się obecnie przy OC.\n" +
                  "<Właściciel OC> = zwykle DummyTraffic utworzony przez grę, a nie samochód mieszkańca.\n" +
                  "<Poza miastem> = gospodarstwo dojeżdżające, turystyczne lub wyprowadzające się.\n" +
                  "<Brak> = brak właściciela lub właściciel nie jest gospodarstwem." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "RAPORT LOGU" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Zapisuje liczbę mieszkańców i pojazdów oraz przykładowe **identyfikatory encji** do CitizenCleaner.log.\n" +
                  "Skopiuj ID do moda **Scene Explorer**, aby je sprawdzić." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "OTWÓRZ LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)),
                  "Otwiera **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Kliknij [Odśwież liczniki]" },
                { "CitizenCleaner/Prompt/NoCity", "Nie wczytano miasta" },
                { "CitizenCleaner/Prompt/Error", "Błąd" },
                { "CitizenCleaner/Status/Progress", "Czyszczenie w toku… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Czyszczenie… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} aktywne | {1} zaparkowane | {2} razem | aktualizacja {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} ulica | {1} obiekt | {2} OC | {3} inne" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} miasto | {1} przy OC | {2} właściciel OC | {3} poza miastem | {4} brak" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — RAPORT LOGU\n" +
                  "Utworzono: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[PORÓWNANIE LICZBY MIESZKAŃCÓW — GRA 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Liczniki gry 1.6 służą tylko do diagnostyki; CC używa własnej liczby do czyszczenia.\n" +
                  "ValidCitizen to flaga ludności wprowadzonej do miasta, a nie test uszkodzonych mieszkańców CC.\n" +
                  "Gra liczy wyprowadzające się i dojeżdżające gospodarstwa; CC liczy mieszkańców." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[SPRAWDZENIE KWALIFIKACJI BEZDOMNYCH]" },
                { "CitizenCleaner/Report/GameCountsPending", "Liczniki gry są nadal inicjalizowane." },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[ID ENCJI MIESZKAŃCÓW — użyj Scene Explorer; Indeks:Wersja]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Uszkodzeni mieszkańcy" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Wyprowadzający się mieszkańcy (gospodarstwo MovingAway + brak PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Dojeżdżający mieszkańcy" },
                { "CitizenCleaner/Report/HomelessCitizens", "Kwalifikujący się bezdomni mieszkańcy" },
                { "CitizenCleaner/Report/IdsLabel", "ID: " },
                { "CitizenCleaner/Report/None", "(brak)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[POJAZDY OSOBISTE]\n" +
                  "Migawka pojazdów jest niedostępna.\n" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nazwa moda" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Wyświetlana nazwa tego moda." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Wersja" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Bieżąca wersja moda." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Wersja informacyjna" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Wersja moda z identyfikatorem commita." },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Strona Paradox Mods; otwiera się w przeglądarce." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)), "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)), "Repozytorium moda na GitHubie; otwiera się w przeglądarce." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)), "Czat Discord do przekazywania opinii; otwiera się w przeglądarce." },
               
                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "UŻYCIE" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Najpierw wykonaj kopię zapasową zapisu!>\n" +
                  "2. <Kliknij [Odśwież liczniki], aby zobaczyć bieżące statystyki.>\n" +
                  "3. [ ✓ ] <Zaznacz pola elementów, które mają zostać uwzględnione>\n" +
                  "4. <Kliknij [Wyczyść mieszkańców], aby usunąć encje.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Uwagi:\n" +
                  "• Ten mod **nie** działa automatycznie; za każdym razem użyj **[Wyczyść mieszkańców]**.\n" +
                  "• W razie nieoczekiwanego zachowania wróć do oryginalnego zapisu miasta." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Zapisz ID encji" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Zapisuje przykłady **25 uszkodzonych**, **10 wyprowadzających się, 10 dojeżdżających i 10 bezdomnych mieszkańców**.\n" +
                  "Zapisuje także ID encji podejrzanych pojazdów.\n" +
                  "Użyj moda **Scene Explorer**, aby sprawdzić ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Użyj [Zapisz ID encji], [Otwórz log], a następnie skopiuj ID encji do moda Scene Explorer w mieście." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Otwórz log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Otwiera **Logs/CitizenCleaner.log** lub folder Logs, jeśli plik jest niedostępny." },

            };
        }
        public void Unload() { }
    }
}
