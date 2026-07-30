// Localization/LocaleZH_HANT.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource
    using Colossal.IO.AssetDatabase.Internal;

    /// <summary>
    /// Traditional Chinese locale (zh-HANT)
    /// </summary>
    public class LocaleZH_HANT : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleZH_HANT(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "操作" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "關於" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "偵錯" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "清理目標" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "操作" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "狀態" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "資訊" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "偵錯" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 異常市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "啟用時（預設），統計**異常**市民。\n" +
                  "這些市民屬於沒有 PropertyRenter 的家庭，且不是無家可歸者、通勤者、遊客或正在搬離者。\n\n" +
                  "- **廢棄汽車：**異常市民和廢棄汽車是主要清理目標。\n" +
                  "- 當家庭中沒有剩餘成員時，遊戲應移除其私人車輛並釋放停車位。\n" +
                  "- CC 將市民標記為刪除；遊戲的清理系統會處理車輛、學校、病患及其他參照。" },


                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ 搬離中（租金 = 0）" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "啟用後，會計算並清理租金為 0（沒有 PropertyRenter 元件）的**搬離中**市民。\n\n" +
                  "仍有 PropertyRenter 或租金大於 0 的搬離中市民不會被移除。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ 通勤者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "啟用後，會計算並清理**通勤者**。通勤者不住在城市內，但會進城工作。\n\n" +
                  "部分通勤者過去可能住在城內，後來因無家可歸而搬出（遊戲 1.2.5 版新增的機制）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ 無家可歸者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "統計並清理帶有 **ValidCitizen + Homeless** 標記的存活市民。\n" +
                  "死亡者、遊客、通勤者和缺少 ValidCitizen 的市民會被排除。\n\n" +
                  "<請謹慎>：刪除無家可歸者可能造成未知副作用。" },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "清理市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "請先載入已儲存的城市。\n移除來自已沒有 PropertyRenter 元件之家庭的市民。\n" +
                  "清理也會包含已勾選 [ ✓ ] 的選用項目。\n\n" +
                  "**請小心**：這是暫時解決方法，可能損壞其他資料。請先備份存檔！" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "永久刪除選項中勾選的項目。\n\n請先備份存檔！\n 是否繼續？" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "重新整理計數" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<請先載入已儲存的城市以取得數字。>\n" +
                  "更新所有實體計數並顯示目前的城市統計資料。\n" +
                  "清理後，請讓遊戲解除暫停並執行一分鐘。" },

                // Cleanup Status and Counts
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "狀態" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "顯示清理狀態。清理進行時會即時更新；其他時候請按 [重新整理計數] 重新計算。\n\n" +
                  "\"**閒置**\" = 沒有進行清理，或尚未載入城市。\n" +
                  "\"**沒有可清理項目**\" = 沒有市民符合所選篩選條件。\n" +
                  "\"**完成**\" = 上次清理已完成；狀態會保留到變更篩選條件或再次清理。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "市民總數" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "**目前模擬中**的市民實體總數。\n\n" +
                  "此數字可能與人口不同，因為其中可能包含異常實體。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "要清理的市民：請勾選上方項目 [ ✓ ]" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "按下 **[清理]** 時要移除的市民實體數量，\n\n" +
                  "依照已勾選的項目 [ ✓ ] 計算。" },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "汽車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "僅統計私人汽車；自行車組車輛和拖車另行報告。\n" +
                  "<運作中> = 位於車道上且未停放；可能正在行駛或停車等待。\n" +
                  "<已停放> = 所有已停放的私人汽車。\n" +
                  "<總計> = 運作中、已停放和過渡狀態的私人汽車。\n" +
                  "<更新> = 這些數量的重新整理時間。\n\n" +
                  "開啟選項時城市模擬會暫停。重新整理前請先執行城市以查看變化。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "已停放汽車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<街道> = 在街道 ParkingLane 上可見的已停放汽車。\n" +
                  "<設施> = 位於建築、車庫或停車設施內的汽車。\n" +
                  "<OC> = 位於外部連接且被隱藏的汽車。\n" +
                  "<其他> = 不符合上述位置的已停放汽車；部分沒有指派停車車道。\n" +
                  "<無車道> 本身並不表示汽車已被廢棄。\n\n" +
                  "使用 **[記錄檔報告]**，然後使用 **[開啟記錄檔]** 查看詳細資訊和實體 ID。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC 汽車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "位於外部連接且被隱藏的汽車，依所有者分組。\n" +
                  "<城市> = 所有者是城市家庭。\n" +
                  "<位於 OC> = 所有者家庭目前位於 OC。\n" +
                  "<OC 所有者> = 通常是遊戲產生的 DummyTraffic，不是居民汽車。\n" +
                  "<外來> = 通勤、遊客或正在搬離的家庭。\n" +
                  "<缺少> = 沒有所有者，或所有者不是家庭。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "記錄檔報告" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "將市民和車輛數量以及範例**實體 ID** 寫入 CitizenCleaner.log。\n" +
                  "將 ID 複製到 **Scene Explorer** 模組中進行檢查。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "開啟記錄檔" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "開啟 **CitizenCleaner.log**。" },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "按一下 [重新整理計數]" },
                { "CitizenCleaner/Prompt/NoCity", "尚未載入城市" },
                { "CitizenCleaner/Prompt/Error", "錯誤" },
                { "CitizenCleaner/Status/Progress", "正在清理… {0}" },
                { "CitizenCleaner/Status/Cleaning", "清理中… {0}" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} 運作中 | {1} 已停放 | {2} 總計 | 更新 {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} 街道 | {1} 設施 | {2} OC | {3} 其他" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2", "{0} 城市 | {1} 位於 OC | {2} OC 所有者 | {3} 外來 | {4} 缺少" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — 記錄檔報告\n" +
                  "產生時間：{0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[市民數量交叉檢查 — 遊戲 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "遊戲 1.6 計數器僅供診斷；CC 使用自己的清理數量。\n" +
                  "ValidCitizen 是已遷入人口標記，不是 CC 的異常市民判定。\n" +
                  "遊戲的搬離和通勤數值統計家庭；CC 統計市民。" },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[無家可歸者資格檢查]" },
                { "CitizenCleaner/Report/GameCountsPending", "遊戲計數仍在初始化。" },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[市民實體 ID — 使用 Scene Explorer；索引:版本]" },
                { "CitizenCleaner/Report/CorruptCitizens", "異常市民" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "正在搬離的市民（家庭 MovingAway + 無 PropertyRenter）" },
                { "CitizenCleaner/Report/CommuterCitizens", "通勤市民" },
                { "CitizenCleaner/Report/HomelessCitizens", "符合條件的無家可歸市民" },
                { "CitizenCleaner/Report/IdsLabel", "ID：" },
                { "CitizenCleaner/Report/None", "（無）" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[私人車輛]\n" +
                  "車輛快照無法使用。\n" },


                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "模組名稱" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "此模組的顯示名稱。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "目前的模組版本。" },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "資訊版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "包含提交 ID 的模組版本。" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods 網站；在瀏覽器中開啟。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)), "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)), "模組的 GitHub 儲存庫；在瀏覽器中開啟。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)), "用於提供模組意見的 Discord 聊天；在瀏覽器中開啟。" },
               
                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "使用方法" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <請先備份存檔！>\n" +
                  "2. <按一下 [重新整理計數] 查看目前統計資料。>\n" +
                  "3. [ ✓ ] <使用核取方塊選擇要包含的項目>\n" +
                  "4. <按一下 [清理市民] 來清理實體。>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "注意事項：\n" +
                  "• 本模組**不會**自動執行；每次移除都必須使用 **[清理市民]**。\n" +
                  "• 如果發生非預期行為，請還原原始城市存檔。" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "記錄實體 ID" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "記錄 **25 個異常市民**、**10 個正在搬離、10 個通勤和 10 個無家可歸市民**的範例。\n" +
                  "也會記錄可疑車輛的實體 ID。\n" +
                  "使用 **Scene Explorer** 模組檢查 ID。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "依序使用[記錄實體 ID]、[開啟記錄檔]，然後在城市中將實體 ID 複製到 Scene Explorer 模組。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "開啟記錄檔" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "開啟 **Logs/CitizenCleaner.log**；若檔案無法使用，則開啟 Logs 資料夾。" },

            };
        }
        public void Unload() { }
    }
}
