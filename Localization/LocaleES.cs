// Localization/LocaleES.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Spanish locale (es-ES)
    /// </summary>
    public class LocaleES : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleES(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Acciones" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "Info" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Depuración" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Grupos a eliminar" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Acciones" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "ESTADO" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Depuración" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Ciudadanos corruptos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Cuando está activado (predeterminado), cuenta los ciudadanos **corruptos**.\n" +
                  "Pertenecen a hogares sin PropertyRenter y no son personas sin hogar, pendulares, turistas ni ciudadanos que se mudan.\n\n" +
                  "- **Coches abandonados:** los ciudadanos corruptos y los coches abandonados son el objetivo principal.\n" +
                  "- Cuando no queda ningún miembro del hogar, el juego debería eliminar su vehículo personal y liberar la plaza.\n" +
                  "- CC marca los ciudadanos para su eliminación; los sistemas del juego gestionan las referencias de vehículos, escuelas, pacientes y otras." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ En mudanza (salida, Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Si está activado, cuenta y elimina a los ciudadanos con estado **En mudanza** y Rent = 0 (es decir, sin componente PropertyRenter).\n\n" +
                  "Los ciudadanos en mudanza con PropertyRenter o con Rent > 0 no se eliminan." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Pendulares" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Cuando está activado, cuenta y limpia a los **Pendulares**. Los pendulares no viven en tu ciudad, pero se desplazan para trabajar.\n\n" +
                  "A veces vivían aquí y se marcharon por quedarse sin hogar (función añadida en la versión 1.2.5 del juego)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Personas sin hogar" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Cuenta y limpia miembros de **HomelessHousehold**.\n\n" +
                  "Eliminar personas sin hogar cambia la población y la demanda residencial.\n" +
                  "Más hogares sin vivienda reducen la demanda general, pero aumentan el factor positivo de alta densidad." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Limpiar ciudadanos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "<Carga primero una ciudad guardada.>\nElimina ciudadanos de hogares que ya no tienen el componente PropertyRenter.\n" +
                  "La limpieza también incluye cualquier elemento opcional marcado [ ✓ ].\n\n" +
                  "**CUIDADO**: esto es un apaño y puede corromper otros datos. ¡Haz una copia de seguridad de tu partida primero!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Los elementos seleccionados en las opciones se eliminarán de forma permanente.\n\n<Por favor, haz antes una copia de seguridad.>\n¿Continuar?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Actualizar recuentos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Carga primero una ciudad para obtener cifras.>\n" +
                  "Actualiza todos los contadores para mostrar las estadísticas actuales de la ciudad.\n" +
                  "Después de limpiar, deja el juego sin pausa durante un minuto." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Estado" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Muestra el estado de la limpieza. Se actualiza en vivo durante una limpieza activa; en otro caso pulsa [Actualizar recuentos] para recalcular.\n\n" +
                  "\"**Idle**\" = no hay limpieza en curso o aún no hay ciudad cargada.\n" +
                  "\"**Nothing to clean**\" = ningún ciudadano coincide con los filtros seleccionados (o ya los eliminaste).\n" +
                  "\"**Complete**\" = la última limpieza terminó; permanece hasta que cambies filtros o ejecutes una nueva limpieza." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Total de ciudadanos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Número total de entidades de ciudadanos **actualmente en la simulación.**\n\n" +
                  "Puede diferir de tu población porque puede incluir entidades corruptas." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Ciudadanos a limpiar: marca [ ✓ ] arriba" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Número de entidades de ciudadanos que se eliminarán al pulsar **[Limpiar ciudadanos]**, \n\n" +
                  "según las casillas [ ✓ ] seleccionadas." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "Vivienda" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "Recuentos actuales de hogares; se actualizan con [Actualizar recuentos].\n" +
                  "<Buscando> = hogares con PropertySeeker activado, incluidos hogares sin vivienda.\n" +
                  "<Entrando/saliendo> = contadores de hogares del juego 1.6.\n" +
                  "PropertySeeker significa que buscan ahora, no que la búsqueda haya fallado." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Coches" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Solo coches personales; los vehículos del grupo de bicicletas y los remolques se muestran por separado.\n" +
                  "<Activos> = están en un carril y no aparcados; pueden estar circulando o detenidos.\n" +
                  "<Aparcados> = todos los coches personales aparcados.\n" +
                  "<Total> = coches personales activos, aparcados y en transición.\n" +
                  "<Actualizado> = hora de actualización de los datos.\n\n" +
                  "La simulación se pausa en Opciones. Ejecuta la ciudad antes de actualizar para ver cambios." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Coches aparcados" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Calle> = coches visibles aparcados en una ParkingLane de la calle.\n" +
                  "<Instalación> = coches en un edificio, garaje o instalación de aparcamiento.\n" +
                  "<OC> = coches ocultos en una conexión exterior.\n" +
                  "<Otros> = coches aparcados no incluidos arriba; algunos no tienen carril de aparcamiento asignado.\n" +
                  "<Sin carril> por sí solo no significa que el coche esté abandonado.\n\n" +
                  "Usa **[INFORME DEL LOG]** y después **[ABRIR LOG]** para ver detalles e IDs de entidades." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Coches en OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Coches ocultos en una conexión exterior, agrupados por propietario.\n" +
                  "<Ciudad> = el propietario es un hogar de la ciudad.\n" +
                  "<En OC> = el hogar propietario está actualmente en una OC.\n" +
                  "<Propietario OC> = normalmente DummyTraffic creado por el juego, no un coche residente.\n" +
                  "<Fuera> = hogar pendular, turista o en mudanza.\n" +
                  "<Falta> = sin propietario o el propietario no es un hogar." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "INFORME DEL LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Escribe recuentos de ciudadanos y vehículos, más ejemplos de **IDs de entidades**, en CitizenCleaner.log.\n" +
                  "Copia un ID en el mod **Scene Explorer** para inspeccionarlo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "ABRIR LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Abre **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Haz clic en [Actualizar recuentos]" },
                { "CitizenCleaner/Prompt/NoCity", "No hay ciudad cargada" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Limpieza en curso… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Limpiando… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} buscando | {1} entrando | {2} saliendo" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} activos | {1} aparcados | {2} total | actualizado {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} calle | {1} instalación | {2} OC | {3} otros" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} ciudad | {1} en OC | {2} propietario OC | {3} fuera | {4} falta" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — INFORME DEL LOG\n" +
                  "Generado: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[COMPROBACIÓN DEL RECUENTO DE CIUDADANOS — JUEGO 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Los contadores del juego 1.6 son solo diagnósticos; CC usa su propio recuento de limpieza.\n" +
                  "ValidCitizen es una marca de población que ya se mudó a la ciudad, no la prueba de ciudadanos corruptos de CC.\n" +
                  "El juego cuenta hogares que se mudan y hogares pendulares; CC cuenta ciudadanos." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[COMPROBACIÓN DE POBLACIÓN SIN HOGAR]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[ESTADOS DE VIVIENDA DE LOS HOGARES]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "Estos estados pueden solaparse. PropertySeeker significa búsqueda, no fallo. La regla actual de CC para corruptos no los excluye." },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "Hogares sin PropertyRenter y no MovedIn" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "Hogares sin PropertyRenter y con PropertySeeker habilitado" },
                { "CitizenCleaner/Report/GameCountsPending", "Los recuentos del juego todavía se están inicializando." },
                { "CitizenCleaner/Report/CitizenIdsHeading",
                  "[IDS DE ENTIDADES DE CIUDADANOS — usa Scene Explorer; Índice:Versión]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Ciudadanos corruptos" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Ciudadanos que se mudan (hogar MovingAway + sin PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Ciudadanos pendulares" },
                { "CitizenCleaner/Report/HomelessCitizens", "Candidatos sin hogar para limpieza" },
                { "CitizenCleaner/Report/IdsLabel", "IDs: " },
                { "CitizenCleaner/Report/None", "(ninguno)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[VEHÍCULOS PERSONALES]\n" +
                  "Instantánea de vehículos no disponible.\n" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nombre del mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Nombre visible de este mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Versión" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Versión actual del mod." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Versión informativa" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Versión del mod con ID de commit" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Página de Paradox Mods; se abre en el navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Repositorio del mod en GitHub; se abre en el navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Canal de Discord para comentarios; se abre en el navegador." },
               
                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "USO" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <¡Haz primero una copia de seguridad de tu partida!>\n" +
                  "2. <Haz clic en [Actualizar recuentos] para ver las estadísticas actuales.>\n" +
                  "3. [ ✓ ] <Selecciona los elementos a incluir con las casillas>\n" +
                  "4. <Haz clic en [Limpiar Ciudadanos] para iniciar la limpieza.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Notas:\n" +
                  "• Este mod **no** se ejecuta automáticamente; usa **[Limpiar ciudadanos]** cada vez que quieras eliminar.\n" +
                  "• Vuelve a tu ciudad guardada original si es necesario por comportamiento inesperado." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Registrar IDs de entidades" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Registra ejemplos de **25 ciudadanos corruptos**, **10 que se mudan, 10 pendulares y 10 sin hogar**.\n" +
                  "También registra IDs de entidades de vehículos sospechosos.\n" +
                  "Usa el mod **Scene Explorer** para inspeccionar un ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Usa [Registrar IDs de entidades], [Abrir log] y copia un ID de entidad en el mod Scene Explorer dentro de la ciudad." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Abrir log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Abre **Logs/CitizenCleaner.log** o la carpeta Logs si el archivo no está disponible." },

            };
        }
        public void Unload() { }
    }
}
