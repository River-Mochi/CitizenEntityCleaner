// Localization/LocalePT_BR.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Brazilian Portuguese locale (pt-BR)
    /// </summary>
    public class LocalePT_BR : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocalePT_BR(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Ações" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "About" },   // keep label in English
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Depuração" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Alvos da limpeza" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Ações" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "STATUS" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Informações" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Depuração" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Cidadãos corrompidos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Quando ativado (padrão), conta cidadãos **corrompidos**.\n" +
                  "Eles pertencem a famílias sem PropertyRenter e não são sem-teto, commuters, turistas ou pessoas de mudança.\n\n" +
                  "- **Carros abandonados:** cidadãos corrompidos e carros abandonados são o alvo principal.\n" +
                  "- Quando nenhum membro da família permanece, o jogo deve remover o veículo pessoal e liberar a vaga.\n" +
                  "- O CC marca cidadãos para exclusão; os sistemas de limpeza do jogo cuidam das referências a veículos, escolas, pacientes e outras." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ Mudando-se (Aluguel = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Quando ativado, conta e limpa cidadãos que estão **Mudando-se** com Aluguel = 0 (ou seja, sem o componente PropertyRenter).\n\n" +
                  "Cidadãos Mudando-se com PropertyRenter ou Aluguel > 0 não são removidos." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Commuters" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Quando ativado, conta e limpa **commuters**. Commuters incluem cidadãos que não moram na sua cidade, mas viajam para trabalhar nela.\n\n" +
                  "Às vezes, commuters já moraram na cidade e se mudaram por virarem sem-teto (recurso adicionado na versão 1.2.5 do jogo)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Sem-teto" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Conta e limpa membros de **HomelessHousehold**.\n\n" +
                  "Excluir pessoas sem-teto altera a população e a demanda residencial.\n" +
                  "Mais famílias sem-teto reduzem a demanda geral, mas aumentam o fator positivo de alta densidade." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Limpar cidadãos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Carregue uma cidade salva antes.\nRemove cidadãos de domicílios que não têm mais o componente PropertyRenter.\n" +
                  "A limpeza também inclui quaisquer itens opcionais marcados [ ✓ ].\n\n" +
                  "**CUIDADO**: isto é um contorno e pode corromper outros dados. Faça backup do seu save primeiro!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Excluir permanentemente os itens selecionados nas opções.\n\nFaça backup do seu save antes!\n Continuar?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Atualizar contagens" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Carregue uma cidade salva para ver os números.>\n" +
                  "Atualiza todas as contagens para mostrar as estatísticas atuais da cidade.\n" +
                  "Após a limpeza, deixe o jogo rodar sem pausa por um minuto." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Status" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Mostra o status da limpeza. Atualiza em tempo real durante uma limpeza ativa; caso contrário, pressione [Atualizar contagens] para recalcular.\n\n" +
                  "\"**Parado**\" = nenhuma limpeza em execução ou nenhuma cidade carregada.\n" +
                  "\"**Nada para limpar**\" = nenhum cidadão corresponde aos filtros selecionados (ou você já os removeu).\n" +
                  "\"**Concluído**\" = a última limpeza terminou; permanece até você mudar os filtros ou iniciar uma nova limpeza." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Total de cidadãos" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Número total de entidades de cidadãos **atualmente na simulação.**\n\n" +
                  "Esse número pode diferir da sua população porque pode incluir entidades corrompidas." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Cidadãos a limpar: selecione [ ✓ ] acima" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Quantidade de entidades de cidadãos que serão removidas ao clicar em **[Limpar cidadãos]**,\n\n" +
                  "com base nas caixas selecionadas [ ✓ ]." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "Moradia" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "Contagens atuais de famílias; atualizadas com [Atualizar contagens].\n" +
                  "<Procurando> = famílias com PropertySeeker ativado, incluindo famílias sem-teto.\n" +
                  "<Chegando/saindo> = contadores de famílias do jogo 1.6.\n" +
                  "PropertySeeker significa procura ativa, não uma busca que falhou." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Carros" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Apenas carros pessoais; veículos do grupo de bicicletas e reboques são informados separadamente.\n" +
                  "<Ativos> = estão em uma faixa e não estacionados; podem estar em movimento ou parados.\n" +
                  "<Estacionados> = todos os carros pessoais estacionados.\n" +
                  "<Total> = carros pessoais ativos, estacionados e em transição.\n" +
                  "<Atualizado> = horário da atualização dos números.\n\n" +
                  "A simulação fica pausada nas Opções. Execute a cidade antes de atualizar para ver mudanças." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Carros estacionados" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Rua> = carros visíveis estacionados em uma ParkingLane da rua.\n" +
                  "<Instalação> = carros em um edifício, garagem ou estacionamento.\n" +
                  "<OC> = carros ocultos em uma conexão externa.\n" +
                  "<Outros> = carros estacionados não classificados acima; alguns não têm faixa de estacionamento atribuída.\n" +
                  "<Sem faixa> por si só não significa que o carro esteja abandonado.\n\n" +
                  "Use **[RELATÓRIO DO LOG]** e depois **[ABRIR LOG]** para detalhes e IDs de entidades." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Carros na OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Carros ocultos em uma conexão externa, agrupados por proprietário.\n" +
                  "<Cidade> = o proprietário é uma família da cidade.\n" +
                  "<Na OC> = a família proprietária está atualmente em uma OC.\n" +
                  "<Proprietário OC> = normalmente DummyTraffic criado pelo jogo, não um carro de morador.\n" +
                  "<Fora> = família commuter, turista ou de mudança.\n" +
                  "<Ausente> = sem proprietário ou o proprietário não é uma família." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "RELATÓRIO DO LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Grava contagens de cidadãos e veículos, além de exemplos de **IDs de entidades**, em CitizenCleaner.log.\n" +
                  "Copie um ID para o mod **Scene Explorer** para inspecioná-lo." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "ABRIR LOG" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Abre **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Clique em [Atualizar contagens]" },
                { "CitizenCleaner/Prompt/NoCity", "Nenhuma cidade carregada" },
                { "CitizenCleaner/Prompt/Error",  "Erro" },
                { "CitizenCleaner/Status/Progress", "Limpeza em andamento… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Limpando… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} procurando | {1} chegando | {2} saindo" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} ativos | {1} estacionados | {2} total | atualizado {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} rua | {1} instalação | {2} OC | {3} outros" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} cidade | {1} na OC | {2} proprietário OC | {3} fora | {4} ausente" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — RELATÓRIO DO LOG\n" +
                  "Gerado: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[COMPARAÇÃO DA CONTAGEM DE CIDADÃOS — JOGO 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Os contadores do jogo 1.6 são apenas diagnósticos; o CC usa sua própria contagem de limpeza.\n" +
                  "ValidCitizen é uma marca de população que já se mudou para a cidade, não o teste de cidadãos corrompidos do CC.\n" +
                  "O jogo conta famílias de mudança e commuters; o CC conta cidadãos." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[COMPARAÇÃO DA POPULAÇÃO SEM-TETO]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[ESTADOS DE MORADIA DAS FAMÍLIAS]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "Esses estados podem se sobrepor. PropertySeeker significa busca, não falha. A regra atual de CC para cidadãos corrompidos não os exclui." },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "Famílias sem PropertyRenter e sem MovedIn" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "Famílias sem PropertyRenter e com PropertySeeker ativado" },
                { "CitizenCleaner/Report/GameCountsPending", "As contagens do jogo ainda estão sendo inicializadas." },
                { "CitizenCleaner/Report/CitizenIdsHeading",
                  "[IDS DE ENTIDADES DE CIDADÃOS — use Scene Explorer; Índice:Versão]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Cidadãos corrompidos" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "Cidadãos de mudança (família MovingAway + sem PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Cidadãos commuters" },
                { "CitizenCleaner/Report/HomelessCitizens", "Candidatos sem-teto para limpeza" },
                { "CitizenCleaner/Report/IdsLabel", "IDs: " },
                { "CitizenCleaner/Report/None", "(nenhum)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[VEÍCULOS PESSOAIS]\n" +
                  "Snapshot de veículos indisponível.\n" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Nome do mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Nome exibido deste mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Versão" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Versão atual do mod." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Versão informativa" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Versão do mod com o ID do commit" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Site Paradox Mods; abre no navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Repositório do mod no GitHub; abre no navegador." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord para feedback sobre o mod; abre no navegador." },
               
                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "USO" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Faça backup do save primeiro!>\n" +
                  "2. <Clique em [Atualizar contagens] para ver as estatísticas atuais.>\n" +
                  "3. [ ✓ ] <Marque os itens a incluir usando as caixas>\n" +
                  "4. <Clique em [Limpar cidadãos] para limpar as entidades.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Notas:\n" +
                  "• Este mod **não** roda automaticamente; use **[Limpar cidadãos]** sempre que quiser remover.\n" +
                  "• Volte ao save original se precisar por comportamento inesperado." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Registrar IDs de entidades" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Registra exemplos de **25 cidadãos corrompidos**, **10 de mudança, 10 commuters e 10 sem-teto**.\n" +
                  "Também registra IDs de entidades de veículos suspeitos.\n" +
                  "Use o mod **Scene Explorer** para inspecionar um ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Use [Registrar IDs de entidades], [Abrir log] e copie um ID de entidade para o mod Scene Explorer dentro da cidade." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Abrir log" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Abre **Logs/CitizenCleaner.log** ou a pasta Logs se o arquivo não estiver disponível." },

            };
        }
        public void Unload() { }
    }
}
