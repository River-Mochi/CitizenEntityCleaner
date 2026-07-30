// Localization/LocaleFR.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "ÉTAT" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Infos" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Débogage" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Citoyens corrompus" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Lorsque cette option est activée (par défaut), les citoyens **corrompus** sont comptés.\n" +
                  "Ils appartiennent à des foyers sans PropertyRenter et ne sont ni sans-abri, ni navetteurs, ni touristes, ni en déménagement.\n\n" +
                  "- **Voitures abandonnées :** les citoyens corrompus et les voitures abandonnées sont la cible principale.\n" +
                  "- Lorsqu'il ne reste aucun membre du foyer, le jeu devrait supprimer son véhicule personnel et libérer la place.\n" +
                  "- CC marque les citoyens pour suppression ; les systèmes du jeu gèrent les références aux véhicules, écoles, patients et autres." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ En déménagement (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Si activé, compte et supprime les citoyens ayant le statut **En déménagement** avec Rent = 0 (donc sans composant PropertyRenter).\n\n" +
                  "Les citoyens en déménagement avec PropertyRenter ou avec Rent > 0 ne sont pas supprimés." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Navetteurs" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Lorsqu’activé, compte et nettoie les **Navetteurs**. Les navetteurs ne vivent pas dans votre ville mais y viennent pour travailler.\n\n" +
                  "Parfois, des navetteurs vivaient auparavant dans votre ville puis sont partis à cause du sans-abrisme (fonction ajoutée en version 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Sans-abri" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Compte et nettoie les citoyens vivants marqués **ValidCitizen + Homeless**.\n" +
                  "Les morts, touristes, navetteurs et citoyens sans ValidCitizen sont exclus.\n\n" +
                  "<ATTENTION> : supprimer les sans-abri peut entraîner des effets inconnus." },

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

                // Cleanup Status and Counts
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

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Voitures" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Voitures personnelles uniquement ; les véhicules du groupe vélo et les remorques sont indiqués séparément.\n" +
                  "<Actives> = sur une voie et non stationnées ; elles peuvent rouler ou être arrêtées.\n" +
                  "<Stationnées> = toutes les voitures personnelles stationnées.\n" +
                  "<Total> = voitures personnelles actives, stationnées et en transition.\n" +
                  "<Actualisé> = heure d'actualisation de ces valeurs.\n\n" +
                  "La simulation est en pause dans les Options. Faites tourner la ville avant d'actualiser." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Voitures stationnées" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Rue> = voitures visibles stationnées sur une ParkingLane de rue.\n" +
                  "<Site> = voitures dans un bâtiment, un garage ou un parking.\n" +
                  "<OC> = voitures masquées à une connexion extérieure.\n" +
                  "<Autres> = voitures stationnées non classées ci-dessus ; certaines n'ont aucune voie de stationnement attribuée.\n" +
                  "<Sans voie> seul ne signifie pas qu'une voiture est abandonnée.\n\n" +
                  "Utilisez **[RAPPORT DU LOG]**, puis **[OUVRIR LE LOG]**, pour les détails et les ID d'entités." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Voitures à l'OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Voitures masquées à une connexion extérieure, regroupées par propriétaire.\n" +
                  "<Ville> = le propriétaire est un foyer de la ville.\n" +
                  "<À l'OC> = le foyer propriétaire se trouve actuellement à une OC.\n" +
                  "<Propriétaire OC> = généralement du DummyTraffic créé par le jeu, pas une voiture de résident.\n" +
                  "<Extérieur> = foyer navetteur, touriste ou en déménagement.\n" +
                  "<Manquant> = aucun propriétaire ou le propriétaire n'est pas un foyer." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "RAPPORT DU LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Écrit les nombres de citoyens et de véhicules ainsi que des exemples d'**ID d'entités** dans CitizenCleaner.log.\n" +
                  "Copiez un ID dans le mod **Scene Explorer** pour l'inspecter." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "OUVRIR LE LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Ouvre **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Cliquez sur [Actualiser]" },
                { "CitizenCleaner/Prompt/NoCity", "Aucune ville chargée" },
                { "CitizenCleaner/Prompt/Error",  "Erreur" },
                { "CitizenCleaner/Status/Progress", "Nettoyage en cours… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Nettoyage… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} actives | {1} stationnées | {2} total | actualisé {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} rue | {1} site | {2} OC | {3} autres" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} ville | {1} à l'OC | {2} propriétaire OC | {3} extérieur | {4} manquant" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — RAPPORT DU LOG\n" +
                  "Généré : {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[VÉRIFICATION DU NOMBRE DE CITOYENS — JEU 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Les compteurs du jeu 1.6 servent uniquement au diagnostic ; CC utilise son propre nombre de nettoyage.\n" +
                  "ValidCitizen est un indicateur de population installée, pas le test des citoyens corrompus de CC.\n" +
                  "Le jeu compte les foyers en déménagement et navetteurs ; CC compte les citoyens." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[VÉRIFICATION DE L'ÉLIGIBILITÉ DES SANS-ABRI]" },
                { "CitizenCleaner/Report/GameCountsPending", "Les compteurs du jeu sont encore en cours d'initialisation." },
                { "CitizenCleaner/Report/CitizenIdsHeading",
                  "[ID D'ENTITÉS DE CITOYENS — utilisez Scene Explorer ; Index:Version]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Citoyens corrompus" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Citoyens en déménagement (foyer MovingAway + sans PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Citoyens navetteurs" },
                { "CitizenCleaner/Report/HomelessCitizens", "Citoyens sans-abri admissibles" },
                { "CitizenCleaner/Report/IdsLabel", "ID : " },
                { "CitizenCleaner/Report/None", "(aucun)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[VÉHICULES PERSONNELS]\n" +
                  "Instantané des véhicules indisponible.\n" },


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
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Site Paradox Mods ; s’ouvre dans le navigateur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Dépôt GitHub du mod ; s’ouvre dans le navigateur." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Salon Discord pour les retours sur le mod ; s’ouvre dans le navigateur." },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Journaliser les ID d'entités" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Journalise des exemples de **25 citoyens corrompus**, **10 en déménagement, 10 navetteurs et 10 sans-abri**.\n" +
                  "Journalise aussi les ID d'entités de véhicules suspects.\n" +
                  "Utilisez le mod **Scene Explorer** pour inspecter un ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Utilisez [Journaliser les ID d'entités], [Ouvrir le log], puis copiez un ID d'entité dans le mod Scene Explorer dans la ville." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Ouvrir le log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Ouvre **Logs/CitizenCleaner.log** ou le dossier Logs si le fichier n'est pas disponible." },

            };
        }
        public void Unload() { }
    }
}
