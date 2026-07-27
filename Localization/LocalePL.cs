namespace CitizenCleaner
{
    using System.Collections.Generic;

    using Colossal;

    /// <summary>
    /// Polish locale (pl-PL).
    /// </summary>
    public class LocalePL : IDictionarySource
    {
        private readonly CCSetting m_Setting;

        public LocalePL(CCSetting setting)
        {
            m_Setting = setting;
        }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors,
            Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Działania" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "Informacje" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Debugowanie" },

                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Cele czyszczenia" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Działania" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Stan mieszkańców i pojazdów" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Informacje" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Debugowanie" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Uszkodzeni mieszkańcy" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Po włączeniu (domyślnie) zlicza i usuwa **uszkodzonych** mieszkańców;\n" +
                  "mieszkańców bez komponentu PropertyRenter, którzy nie są bezdomni, dojeżdżający, turystami ani osobami w trakcie wyprowadzki.\n\n" +
                  "Uszkodzeni mieszkańcy są głównym celem tego moda. Zbyt duża ich liczba może z czasem powodować problemy." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ Wyprowadzający się (czynsz = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Po włączeniu zlicza i usuwa mieszkańców **wyprowadzających się** z czynszem równym 0 (bez komponentu PropertyRenter).\n\n" +
                  "Osoby wyprowadzające się, które mają PropertyRenter lub czynsz większy od 0, nie są usuwane." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Dojeżdżający" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Po włączeniu zlicza i usuwa **dojeżdżających**. Nie mieszkają oni w mieście, lecz przyjeżdżają do niego do pracy.\n\n" +
                  "Czasami wcześniej mieszkali w mieście, ale wyprowadzili się z powodu bezdomności (funkcja dodana w wersji gry 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Bezdomni" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Po włączeniu zlicza i usuwa **bezdomnych** mieszkańców.\n\n" +
                  "<UWAGA>: usuwanie bezdomnych może powodować nieznane skutki uboczne." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Wyczyść mieszkańców" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Najpierw wczytaj zapisane miasto.\nUsuwa mieszkańców z gospodarstw domowych, które nie mają już komponentu PropertyRenter.\n" +
                  "Czyszczenie obejmuje także zaznaczone [ ✓ ] elementy opcjonalne.\n\n" +
                  "**UWAGA**: to obejście może uszkodzić inne dane. Najpierw utwórz kopię zapasową zapisu!" },
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Trwale usunąć elementy zaznaczone w opcjach?\n\nNajpierw wykonaj kopię zapasową zapisu!\n Kontynuować?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Odśwież liczniki" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Najpierw wczytaj zapisane miasto, aby zobaczyć liczby.>\n" +
                  "Aktualizuje wszystkie liczniki encji i pokazuje bieżące statystyki miasta.\n" +
                  "Po czyszczeniu uruchom grę bez pauzy na minutę." },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)), "Porównanie liczby mieszkańców" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)),
                  "Porównuje szeroki licznik encji HouseholdMember moda Citizen Cleaner z liczbą prawidłowych mieszkańców wprowadzonych do miasta według gry 1.6.0. " +
                  "Wartości nie muszą być równe, ponieważ licznik gry wyklucza dojeżdżających, turystów, wyprowadzających się oraz inne nieprawidłowe lub nierezydenckie encje." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)), "Samochody osobiste" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)),
                  "Nieusunięte encje PersonalCar, bez encji tymczasowych, zniszczonych, niekontrolowanych, rowerów i przyczep." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)), "Parkowanie samochodów osobistych" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)),
                  "Rozłączne kategorie zaparkowanych pojazdów. Ulica oznacza widoczny ParkingLane; obiekt śledzi łańcuch właścicieli pasa do GarageLane, ParkingFacility, CarParkingFacility lub Building; ukryte przy OC oznacza Unspawned oraz pas lub TripSource przy połączeniu zewnętrznym." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)), "Właściciele aut ukrytych przy OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)),
                  "Rozróżnia gospodarstwo domowe znajdujące się przy połączeniu zewnętrznym od nietypowej sytuacji, gdy Owner jest bezpośrednio encją OC. " +
                  "To dane diagnostyczne, a nie automatyczne cele usuwania." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)), "Dowody postoju ukrytego przy OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)),
                  "Pokazuje, czy auto ukryte przy OC jest powiązane przez zaparkowany pas lub TripSource. " +
                  "TripSource przy OC bez pasa odpowiada początkowemu mechanizmowi gry używanemu, gdy w pobliżu nie ma parkingu, i nie dowodzi porzucenia pojazdu." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleStatusDisplay)), "Rowery" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleStatusDisplay)),
                  "W grze rowery są encjami PersonalCar, ale są wyświetlane osobno, ponieważ korzystają z innej infrastruktury parkingowej." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleParkingDisplay)), "Parkowanie rowerów" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleParkingDisplay)),
                  "Oddziela widoczne zaparkowane rowery od ukrytych rowerów przy połączeniach zewnętrznych i w innych miejscach." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)), "Potencjalnie osierocone pojazdy" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)),
                  "Chwilowe niezgodności własności według tych samych reguł co system PersonalCarOwnerSystem gry. " +
                  "Gra zwykle je usuwa, więc niewielka tymczasowa liczba jest możliwa." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)), "Miejsce postoju potencjalnie osieroconych pojazdów" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)),
                  "Pokazuje miejsca znalezienia zaparkowanych aut z niezgodnością własności. " +
                  "To dane testowe dla przyszłej funkcji czyszczenia; nic nie jest usuwane." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)), "Zaktualizowano" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)),
                  "Dane pojazdów są skanowane raz przy pierwszym odczycie strony opcji, po naciśnięciu [Odśwież liczniki] lub podczas zapisu raportu diagnostycznego. Skanowanie nie działa w każdej klatce." },

                { "CitizenCleaner/Prompt/RefreshCounts", "Kliknij [Odśwież liczniki]" },
                { "CitizenCleaner/Prompt/NoCity", "Nie wczytano miasta" },
                { "CitizenCleaner/Prompt/Error", "Błąd" },
                { "CitizenCleaner/Status/Progress", "Czyszczenie w toku… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Czyszczenie… {0}" },
                { "CitizenCleaner/Status/CitizenCountRow",
                  "Encje członków gospodarstw CC {0} | prawidłowi mieszkańcy gry {1} | różnica {2}" },
                { "CitizenCleaner/Status/CitizenCountPendingRow",
                  "Encje członków gospodarstw CC {0} | liczniki gry są jeszcze inicjalizowane" },
                { "CitizenCleaner/Status/CarSummaryRow",
                  "Łącznie {0} | aktywne {1} | zaparkowane {2} | przejściowe/inne {3}" },
                { "CitizenCleaner/Status/CarParkingRow",
                  "Ulica {0} | budynek/parking {1} (ukryte {2}) | ukryte przy OC {3} | inne {4} (ukryte {5})" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow",
                  "Gospodarstwo w mieście {0} | gospodarstwo przy OC {1} | bezpośredni właściciel OC {2} | nierezydent/wyprowadzka {3} | brak/nie gospodarstwo {4} | niezgodność własności {5}" },
                { "CitizenCleaner/Status/OcHiddenStageRow",
                  "Dowody OC: pas postoju {0} | TripSource {1} | TripSource bez pasa {2} | HomeTarget {3} | keeper przy OC {4}" },
                { "CitizenCleaner/Status/BicycleSummaryRow",
                  "Łącznie {0} | aktywne {1} | zaparkowane {2} | przejściowe/inne {3}" },
                { "CitizenCleaner/Status/BicycleParkingRow",
                  "Widoczne zaparkowane {0} | ukryte przy OC {1} | ukryte gdzie indziej {2}" },
                { "CitizenCleaner/Status/OwnershipRow",
                  "Auta {0}: bez Owner {1} | właściciel bez bufora {2} | brak odnośnika zwrotnego {3} | rowery {4}" },
                { "CitizenCleaner/Status/OwnershipLocationRow",
                  "Zaparkowane niezgodności: ulica {0} | budynek/parking {1} | ukryte przy OC {2} | inne {3}" },
                { "CitizenCleaner/Status/CapturedAtRow", "Czas migawki {0}" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nazwa moda" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Wyświetlana nazwa tego moda." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Wersja" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Bieżąca wersja moda." },
#if DEBUG
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Wersja informacyjna" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Wersja moda z identyfikatorem commita." },
#endif

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)), "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)), "Repozytorium moda na GitHubie; otwiera się w przeglądarce." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)), "Czat Discord do przekazywania opinii; otwiera się w przeglądarce." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Strona Paradox Mods; otwiera się w przeglądarce." },

                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "UŻYCIE" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Najpierw wykonaj kopię zapasową zapisu!>\n" +
                  "2. <Kliknij [Odśwież liczniki], aby zobaczyć bieżące statystyki.>\n" +
                  "3. [ ✓ ] <Zaznacz pola elementów, które mają zostać uwzględnione>\n" +
                  "4. <Kliknij [Wyczyść mieszkańców], aby usunąć encje.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Uwagi:\n" +
                  "• Ten mod **nie** działa automatycznie; za każdym razem użyj **[Wyczyść mieszkańców]**.\n" +
                  "• W razie nieoczekiwanego zachowania wróć do oryginalnego zapisu miasta." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Zapisz raport diagnostyczny do dziennika" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "- Zapisuje uporządkowany raport z **25 identyfikatorami uszkodzonych mieszkańców** oraz po **10 identyfikatorów wyprowadzających się, dojeżdżających i bezdomnych** (Index:Version).\n\n" +
                  "- Zawiera także porównanie liczników gry i CC oraz stan samochodów osobistych i rowerów.\n\n" +
                  "- **Tylko do odczytu** — niczego nie usuwa.\n\n" +
                  "- Plik dziennika:\n" +
                  "%USERPROFILE%/AppData/LocalLow/Colossal Order/Cities Skylines II/logs/CitizenCleaner.log" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Jeden przycisk zapisuje pełny i czytelny raport diagnostyczny. Nic nie jest usuwane." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Otwórz dziennik" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)), "Otwiera plik dziennika w domyślnym edytorze tekstu." },
            };
        }

        public void Unload()
        {
        }
    }
}
