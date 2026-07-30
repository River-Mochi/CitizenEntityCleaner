// Localization/LocaleKO.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

    /// <summary>
    /// Korean locale (ko-KR)
    /// </summary>
    public class LocaleKO : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleKO(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "작업" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "정보" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "디버그" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "정리 대상" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "작업" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "상태" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "정보" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "디버그" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 손상된 시민" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "활성화하면(기본값) **손상된** 시민을 계산합니다.\n" +
                  "PropertyRenter가 없는 가구에 속하며 노숙자, 통근자, 관광객 또는 이주 중인 시민은 아닙니다.\n\n" +
                  "- **방치 차량:** 손상된 시민과 방치 차량이 주요 대상입니다.\n" +
                  "- 가구원이 남아 있지 않으면 게임이 개인 차량을 제거하고 주차 공간을 비워야 합니다.\n" +
                  "- CC는 시민을 삭제 대상으로 표시하며 게임의 정리 시스템이 차량, 학교, 환자 및 기타 참조를 처리합니다." },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ 이주 중 (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "활성화 시, 현재 **이주 중**이며 Rent = 0(= PropertyRenter 없음)인 시민을 집계하고 정리합니다.\n\n" +
                  "PropertyRenter가 있거나 Rent > 0 인 이주 중 시민은 제거되지 않습니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ 통근자" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "활성화 시, **통근자** 시민을 집계하고 정리합니다. 통근자는 이 도시에 거주하지 않지만 일하러 드나드는 사람을 의미합니다.\n\n" +
                  "통근자가 과거에 이 도시에 살았지만 노숙으로 전출되었을 수도 있습니다(게임 버전 1.2.5 기능)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ 노숙자" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "**ValidCitizen + Homeless**로 표시된 생존 시민을 계산하고 정리합니다.\n" +
                  "사망자, 관광객, 통근자 및 ValidCitizen이 없는 시민은 제외됩니다.\n\n" +
                  "<주의>: 노숙자 삭제는 알 수 없는 부작용을 일으킬 수 있습니다." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "시민 정리" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "먼저 저장된 도시를 불러오세요.\nPropertyRenter 구성요소가 더 이상 없는 가구에서 시민을 제거합니다.\n" +
                  "정리에는 [ ✓ ] 로 선택한 선택 항목도 포함됩니다.\n\n" +
                  "**주의**: 이는 우회 방법이므로 다른 데이터가 손상될 수 있습니다. 먼저 저장 파일을 백업하세요!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "옵션에서 선택한 항목을 영구적으로 삭제합니다.\n\n먼저 저장 파일을 백업하세요!\n 계속하시겠습니까?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "개수 새로고침" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "＜수치를 보려면 먼저 저장된 도시를 불러오세요.＞\n" +
                  "모든 엔티티 개수를 갱신하여 현재 도시 통계를 표시합니다.\n" +
                  "정리 후에는 잠시 동안 게임을 일시정지 해제 상태로 두세요." },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "상태" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "정리 상태를 표시합니다. 실행 중에는 실시간으로 갱신되며, 실행 중이 아니면 [개수 새로고침]으로 재계산하세요.\n\n" +
                  "\"**Idle**\" = 정리 실행 중 아님 또는 도시 미로드.\n" +
                  "\"**Nothing to clean**\" = 선택한 필터에 일치하는 시민 없음(또는 이미 제거됨).\n" +
                  "\"**Complete**\" = 마지막 정리 완료. 필터 변경 또는 새 정리 실행 전까지 유지됩니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "전체 시민 수" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "현재 시뮬레이션에 존재하는 시민 엔티티의 총수.\n\n" +
                  "손상 엔티티를 포함할 수 있으므로 인구 수와 다를 수 있습니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "정리할 시민: 위에서 [ ✓ ] 선택" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "「**정리**」를 클릭할 때 제거될 시민 엔티티 수입니다.\n\n" +
                  "선택한 체크박스 [ ✓ ] 에 따라 달라집니다." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "자동차" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "개인 자동차만 표시합니다. 자전거 그룹 차량과 트레일러는 별도로 보고됩니다.\n" +
                  "<활성> = 차선 위에 있고 주차 중이 아님. 이동 중이거나 정지해 있을 수 있습니다.\n" +
                  "<주차> = 주차된 모든 개인 자동차.\n" +
                  "<전체> = 활성, 주차 및 전환 중인 개인 자동차.\n" +
                  "<업데이트> = 이 수치가 갱신된 시간.\n\n" +
                  "옵션 화면에서는 도시 시뮬레이션이 일시 정지됩니다. 변경 사항을 보려면 도시를 실행한 뒤 갱신하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "주차된 자동차" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<도로> = 도로 ParkingLane에 보이는 상태로 주차된 자동차.\n" +
                  "<시설> = 건물, 차고 또는 주차 시설 안의 자동차.\n" +
                  "<OC> = 외부 연결에 숨겨진 자동차.\n" +
                  "<기타> = 위 위치와 일치하지 않는 주차 차량. 일부는 지정된 주차 차선이 없습니다.\n" +
                  "<차선 없음>만으로 방치 차량임을 의미하지 않습니다.\n\n" +
                  "자세한 내용과 엔티티 ID는 **[로그 보고서]**, **[로그 열기]**를 차례로 사용하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC 자동차" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "외부 연결에 숨겨진 자동차를 소유자별로 표시합니다.\n" +
                  "<도시> = 소유자가 도시 가구입니다.\n" +
                  "<OC에 있음> = 소유 가구가 현재 OC에 있습니다.\n" +
                  "<OC 소유자> = 일반적으로 게임이 생성한 DummyTraffic이며 주민 차량이 아닙니다.\n" +
                  "<외부> = 통근자, 관광객 또는 이주 중인 가구입니다.\n" +
                  "<없음> = 소유자가 없거나 소유자가 가구가 아닙니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "로그 보고서" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "시민 및 차량 수와 예시 **엔티티 ID**를 CitizenCleaner.log에 기록합니다.\n" +
                  "ID를 **Scene Explorer** 모드에 복사하여 검사하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "로그 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "**CitizenCleaner.log**를 엽니다." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "[개수 새로고침] 클릭" },
                { "CitizenCleaner/Prompt/NoCity", "도시가 로드되지 않음" },
                { "CitizenCleaner/Prompt/Error",  "오류" },
                { "CitizenCleaner/Status/Progress", "정리 진행 중… {0}" },
                { "CitizenCleaner/Status/Cleaning", "정리 중… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} 활성 | {1} 주차 | {2} 전체 | 업데이트 {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} 도로 | {1} 시설 | {2} OC | {3} 기타" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2", "{0} 도시 | {1} OC에 있음 | {2} OC 소유자 | {3} 외부 | {4} 없음" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — 로그 보고서\n" +
                  "생성: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[시민 수 교차 확인 — 게임 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "게임 1.6 카운터는 진단 전용이며 CC는 자체 정리 수치를 사용합니다.\n" +
                  "ValidCitizen은 전입 완료 인구 플래그이며 CC의 손상 시민 판정이 아닙니다.\n" +
                  "게임의 이주 중 및 통근자 값은 가구 수이고 CC는 시민 수를 셉니다." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[노숙자 대상 조건 확인]" },
                { "CitizenCleaner/Report/GameCountsPending", "게임 수치를 아직 초기화하고 있습니다." },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[시민 엔티티 ID — Scene Explorer 사용; Index:Version]" },
                { "CitizenCleaner/Report/CorruptCitizens", "손상된 시민" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "이주 중인 시민(가구 MovingAway + PropertyRenter 없음)" },
                { "CitizenCleaner/Report/CommuterCitizens", "통근 시민" },
                { "CitizenCleaner/Report/HomelessCitizens", "대상 노숙 시민" },
                { "CitizenCleaner/Report/IdsLabel", "ID: " },
                { "CitizenCleaner/Report/None", "(없음)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[개인 차량]\n" +
                  "차량 스냅샷을 사용할 수 없습니다.\n" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "모드 이름" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "이 모드의 표시 이름." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "현재 모드 버전." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "정보 버전" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "커밋 ID가 포함된 모드 버전" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox Mods 웹사이트. 브라우저에서 열립니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "이 모드의 GitHub 저장소. 브라우저에서 열립니다." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "모드 피드백용 Discord. 브라우저에서 열립니다." },
               
                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "사용법" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. ＜먼저 저장 파일을 백업!＞\n" +
                  "2. ＜[개수 새로고침]을 클릭하여 현재 통계를 확인＞\n" +
                  "3. [ ✓ ] ＜체크박스로 포함할 항목 선택＞\n" +
                  "4. ＜[시민 정리]를 클릭하여 엔티티 정리＞" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "참고:\n" +
                  "• 이 모드는 **자동으로** 실행되지 않습니다. 제거가 필요할 때마다 **[시민 정리]** 를 사용하세요.\n" +
                  "• 예기치 않은 동작이 발생하면 원본 저장으로 되돌리세요." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "엔티티 ID 기록" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "**손상 시민 25명**, **이주 중 10명, 통근자 10명, 노숙자 10명**의 예시를 기록합니다.\n" +
                  "의심 차량의 엔티티 ID도 기록합니다.\n" +
                  "ID를 확인하려면 **Scene Explorer** 모드를 사용하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "[엔티티 ID 기록], [로그 열기]를 누른 뒤 도시 안에서 엔티티 ID를 Scene Explorer 모드에 복사하세요." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "로그 열기" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "**Logs/CitizenCleaner.log**를 열며 파일이 없으면 Logs 폴더를 엽니다." },

            };
        }
        public void Unload() { }
    }
}
