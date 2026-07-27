// LocaleFR.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary

    using Colossal;                    // IDictionarySource

    /// <summary>
    /// French locale (fr-FR)
    /// </summary>
    public class LocaleFR : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleFR(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Actions" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "À propos" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Débogage" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Cibles à nettoyer" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Actions" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Infos" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Débogage" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "Citoyens corrompus" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Lorsqu’activé (par défaut), compte et nettoie les **Citoyens Corrompus** ;\n" +
                  "résidents dépourvus du composant PropertyRenter (et qui ne sont ni sans-abri, ni navetteurs, ni touristes, ni en train de partir).\n\n" +
                  "Les citoyens corrompus sont la cible principale de ce mod. Trop nombreux, ils peuvent poser des problèmes à la longue." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "En déménagement (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Si activé, compte et supprime les citoyens ayant le statut **En déménagement** avec Rent = 0 (donc sans composant PropertyRenter).\n\n" +
                  "Les citoyens en déménagement avec PropertyRenter ou avec Rent > 0 ne sont pas supprimés." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "Navetteurs" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Lorsqu’activé, compte et nettoie les **Navetteurs**. Les navetteurs ne vivent pas dans votre ville mais y viennent pour travailler.\n\n" +
                  "Parfois, des navetteurs vivaient auparavant dans votre ville puis sont partis à cause du sans-abrisme (fonction ajoutée en version 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "Sans-abri" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Lorsque cette option est activée, compte et nettoie les **Sans-Abri**.\n\n" +
                  "<ATTENTION>: supprimer des sans-abri peut entraîner des effets secondaires imprévus." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Nettoyer les citoyens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "<Chargez d’abord une partie enregistrée.>\nSupprime les citoyens des ménages qui n’ont plus le composant PropertyRenter.\n" +
                  "Le nettoyage inclut aussi les éléments optionnels cochés [ ✓ ].\n\n" +
                  "**ATTENTION** : solution de contournement pouvant corrompre d’autres données. Faites d’abord une sauvegarde de votre partie !" },
                
                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Supprime définitivement les éléments cochés dans les options.\n\n<Merci de sauvegarder d’abord !>\nContinuer ?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Actualiser" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Chargez d’abord une partie pour obtenir des chiffres.>\n" +
                  "Actualise tous les compteurs pour afficher les statistiques actuelles de la ville.\n" +
                  "Après le nettoyage, laissez le jeu tourner une minute sans pause." },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Écrire le rapport de diagnostic" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Écrit un rapport lisible : 25 ID corrompues et 10 ID pour les citoyens en déménagement, navetteurs et sans-abri ; plus les comptes et l’état des véhicules.\n\n" +
                  "**Lecture seule** — rien n’est supprimé." },

                // Sentence UNDER the button (multiline)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Un bouton écrit le rapport de diagnostic complet. Rien n’est supprimé." },

                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Statut" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Affiche le statut du nettoyage. Mise à jour en direct pendant un nettoyage actif ; sinon, appuyez sur [Actualiser] pour recalculer.\n\n" +
                  "\"**Idle**\" = aucun nettoyage en cours ou aucune ville chargée.\n" +
                  "\"**Nothing to clean**\" = aucun citoyen ne correspond aux filtres sélectionnés (ou vous les avez déjà supprimés).\n" +
                  "\"**Complete**\" = dernier nettoyage terminé ; persiste jusqu’à ce que vous changiez les filtres ou lanciez un nouveau nettoyage." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Citoyens au total" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Nombre total d’entités citoyens **actuellement dans la simulation.**\n\n" +
                  "Ce nombre peut différer de votre population car il peut inclure des entités corrompues." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Citoyens à nettoyer : cochez [ ✓ ] ci-dessus" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Nombre d’entités citoyens supprimées lorsque vous cliquez sur **[Nettoyer les citoyens]**,\n\n" +
                  "selon les cases [ ✓ ] sélectionnées." },

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
                { "CitizenCleaner/Prompt/RefreshCounts", "Cliquez sur [Actualiser]" },
                { "CitizenCleaner/Prompt/NoCity", "Aucune ville chargée" },
                { "CitizenCleaner/Prompt/Error",  "Erreur" },
                { "CitizenCleaner/Status/Progress", "Nettoyage en cours… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Nettoyage… {0}" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nom du mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Nom d’affichage de ce mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Version" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Version actuelle du mod." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Version informationnelle" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Version du mod avec l’ID de commit" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Dépôt GitHub du mod ; s’ouvre dans le navigateur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Salon Discord pour les retours sur le mod ; s’ouvre dans le navigateur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Site Paradox Mods ; s’ouvre dans le navigateur." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "UTILISATION" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Sauvegardez d’abord votre partie !>\n" +
                  "2. <Cliquez sur [Actualiser] pour voir les stats.>\n" +
                  "3. [ ✓ ] <Sélectionnez les éléments à inclure via les cases>\n" +
                  "4. <Cliquez sur [Nettoyer les citoyens] pour lancer le nettoyage.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Notes :\n" +
                  "• Ce mod ne s’exécute **pas** automatiquement ; utilisez **[Nettoyer les citoyens]** à chaque fois pour supprimer.\n" +
                  "• Revenez à votre sauvegarde d’origine en cas de comportement inattendu." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}
