// LocaleES.cs
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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Info" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Depuración" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "Ciudadanos corruptos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Cuando está activado (predeterminado), cuenta y limpia ciudadanos **Corruptos**;\n" +
                  "residentes que no tienen el componente PropertyRenter (y que no sean sin hogar, pendulares, turistas o en mudanza).\n\n" +
                  "Los ciudadanos corruptos son el objetivo principal de este mod. Si hay demasiados en la ciudad, pueden causar problemas con el tiempo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "En mudanza (salida, Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Si está activado, cuenta y elimina a los ciudadanos con estado **En mudanza** y Rent = 0 (es decir, sin componente PropertyRenter).\n\n" +
                  "Los ciudadanos en mudanza con PropertyRenter o con Rent > 0 no se eliminan." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "Pendulares" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Cuando está activado, cuenta y limpia a los **Pendulares**. Los pendulares no viven en tu ciudad, pero se desplazan para trabajar.\n\n" +
                  "A veces vivían aquí y se marcharon por quedarse sin hogar (función añadida en la versión 1.2.5 del juego)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "Ciudadanos sin hogar" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Cuando está activado, cuenta y limpia a los **Ciudadanos Sin Hogar**.\n\n" +
                  "<CUIDADO>: eliminar ciudadanos sin hogar puede causar efectos inesperados." },

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

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Escribir informe de diagnóstico" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Escribe un informe legible: 25 IDs corruptas y 10 IDs de ciudadanos que se mudan, viajeros y sin hogar; además, recuentos y estado de vehículos.\n\n" +
                  "**Solo lectura** — no elimina nada." },

                // Sentence UNDER the button (multiline)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Un botón escribe el informe completo de diagnóstico. No se elimina nada." },

                // Displays
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
                { "CitizenCleaner/Prompt/RefreshCounts", "Haz clic en [Actualizar recuentos]" },
                { "CitizenCleaner/Prompt/NoCity", "No hay ciudad cargada" },
                { "CitizenCleaner/Prompt/Error",  "Error" },
                { "CitizenCleaner/Status/Progress", "Limpieza en curso… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Limpiando… {0}" },


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
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Repositorio del mod en GitHub; se abre en el navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Canal de Discord para comentarios; se abre en el navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Página de Paradox Mods; se abre en el navegador." },

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
            };
        }

        public void Unload() { }
    }
}
