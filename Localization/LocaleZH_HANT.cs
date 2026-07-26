using System.Collections.Generic;
using Colossal;

namespace CitizenCleaner
{
    /// <summary>
    /// Traditional Chinese locale entries (zh-HANT).
    /// </summary>
    public class LocaleZH_HANT : IDictionarySource
    {
        private readonly CCSetting m_Setting;

        public LocaleZH_HANT(CCSetting setting)
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

                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "操作" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "關於" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "偵錯" },

                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "清理目標" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "操作" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "市民與載具狀態" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "資訊" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "偵錯" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 異常市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "啟用後（預設啟用），會計算並清理**異常**市民；\n" +
                  "也就是缺少 PropertyRenter 元件，且並非無家可歸者、通勤者、遊客或搬離中市民的居民。\n\n" +
                  "異常市民是本模組的主要清理目標。數量過多可能會逐漸造成問題。" },

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
                  "啟用後，會計算並清理**無家可歸**市民。\n\n" +
                  "<請小心>：刪除無家可歸者可能造成未知副作用。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "清理市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "請先載入已儲存的城市。\n移除來自已沒有 PropertyRenter 元件之家庭的市民。\n" +
                  "清理也會包含已勾選 [ ✓ ] 的選用項目。\n\n" +
                  "**請小心**：這是暫時解決方法，可能損壞其他資料。請先備份存檔！" },
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "永久刪除選項中勾選的項目。\n\n請先備份存檔！\n 是否繼續？" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "重新整理計數" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<請先載入已儲存的城市以取得數字。>\n" +
                  "更新所有實體計數並顯示目前的城市統計資料。\n" +
                  "清理後，請讓遊戲解除暫停並執行一分鐘。" },

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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)), "市民計數比較" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CitizenCountComparisonDisplay)),
                  "比較 Citizen Cleaner 的廣義 HouseholdMember 實體計數與遊戲 1.6.0 的有效遷入市民計數。 " +
                  "兩者不一定相同，因為遊戲計數會排除通勤者、遊客、搬離中、無效及其他非居民實體。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)), "私人汽車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarStatusDisplay)),
                  "尚未刪除的 PersonalCar 實體，不包含暫時、已摧毀、失控、自行車及拖車實體。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)), "私人汽車停放狀態" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.PersonalCarParkingDisplay)),
                  "互不重疊的停放分類。路邊使用可見的 ParkingLane；設施會沿停放車道的擁有者鏈尋找 GarageLane、ParkingFacility、CarParkingFacility 或 Building；OC 隱藏則為 Unspawned 加上位於城外連接點的停放車道或 TripSource。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)), "OC 隱藏汽車的擁有者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionOwnerDisplay)),
                  "區分位於城外連接點的家庭，以及 Owner 本身直接是 OC 實體的罕見情況。 " +
                  "這些是診斷資料，不是自動刪除目標。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)), "OC 隱藏暫存證據" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OutsideConnectionStageDisplay)),
                  "顯示 OC 隱藏汽車是否透過停放車道或 TripSource 連結。 " +
                  "OC 的 TripSource 沒有停放車道時，符合遊戲找不到附近停車位時的初始備援機制，並不能證明車輛已被棄置。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleStatusDisplay)), "自行車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleStatusDisplay)),
                  "遊戲將自行車視為 PersonalCar 實體，但因自行車使用不同的停車設施，所以分開顯示。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.BicycleParkingDisplay)), "自行車停放狀態" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.BicycleParkingDisplay)),
                  "分別顯示可見的已停放自行車、位於城外連接點的隱藏自行車，以及其他位置的隱藏自行車。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)), "潛在孤立載具" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipDisplay)),
                  "依照遊戲 PersonalCarOwnerSystem 的相同規則，顯示該時間點的所有權不一致。 " +
                  "遊戲通常會移除這些實體，因此少量暫時計數是可能的。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)), "潛在孤立載具的停放位置" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleOwnershipLocationDisplay)),
                  "顯示所有權不一致之已停放汽車的位置。 " +
                  "這是未來清理功能的測試資料；不會刪除任何項目。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)), "更新時間" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VehicleSnapshotTimeDisplay)),
                  "首次讀取選項頁面、按下 [重新整理計數] 或寫入偵錯報告時，才會掃描一次載具資料。不會逐幀掃描狀態。" },

                { "CitizenCleaner/Prompt/RefreshCounts", "按一下 [重新整理計數]" },
                { "CitizenCleaner/Prompt/NoCity", "尚未載入城市" },
                { "CitizenCleaner/Prompt/Error", "錯誤" },
                { "CitizenCleaner/Status/Progress", "正在清理… {0}" },
                { "CitizenCleaner/Status/Cleaning", "清理中… {0}" },
                { "CitizenCleaner/Status/CitizenCountRow",
                  "CC 家庭成員實體 {0} | 遊戲有效遷入市民 {1} | 差異 {2}" },
                { "CitizenCleaner/Status/CitizenCountPendingRow",
                  "CC 家庭成員實體 {0} | 遊戲計數仍在初始化" },
                { "CitizenCleaner/Status/CarSummaryRow",
                  "總數 {0} | 活動中 {1} | 已停放 {2} | 轉換中/其他 {3}" },
                { "CitizenCleaner/Status/CarParkingRow",
                  "路邊 {0} | 建築物/停車設施 {1}（隱藏 {2}）| OC 隱藏 {3} | 其他 {4}（隱藏 {5}）" },
                { "CitizenCleaner/Status/OcHiddenOwnerRow",
                  "城市家庭 {0} | 位於 OC 的家庭 {1} | 直接 OC 擁有者 {2} | 非居民/搬離中 {3} | 缺少/非家庭 {4} | 所有權不一致 {5}" },
                { "CitizenCleaner/Status/OcHiddenStageRow",
                  "OC 證據：停放車道 {0} | TripSource {1} | 無車道的 TripSource {2} | HomeTarget {3} | 位於 OC 的 keeper {4}" },
                { "CitizenCleaner/Status/BicycleSummaryRow",
                  "總數 {0} | 活動中 {1} | 已停放 {2} | 轉換中/其他 {3}" },
                { "CitizenCleaner/Status/BicycleParkingRow",
                  "可見停放 {0} | OC 隱藏 {1} | 其他位置隱藏 {2}" },
                { "CitizenCleaner/Status/OwnershipRow",
                  "汽車 {0}：沒有 Owner {1} | 擁有者沒有緩衝區 {2} | 缺少反向連結 {3} | 自行車 {4}" },
                { "CitizenCleaner/Status/OwnershipLocationRow",
                  "已停放的不一致：路邊 {0} | 建築物/停車設施 {1} | OC 隱藏 {2} | 其他 {3}" },
                { "CitizenCleaner/Status/CapturedAtRow", "快照時間 {0}" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "模組名稱" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "此模組的顯示名稱。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "目前的模組版本。" },
#if DEBUG
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "資訊版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "包含提交 ID 的模組版本。" },
#endif

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)), "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)), "模組的 GitHub 儲存庫；在瀏覽器中開啟。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)), "用於提供模組意見的 Discord 聊天；在瀏覽器中開啟。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods 網站；在瀏覽器中開啟。" },

                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "使用方法" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <請先備份存檔！>\n" +
                  "2. <按一下 [重新整理計數] 查看目前統計資料。>\n" +
                  "3. [ ✓ ] <使用核取方塊選擇要包含的項目>\n" +
                  "4. <按一下 [清理市民] 來清理實體。>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "注意事項：\n" +
                  "• 本模組**不會**自動執行；每次移除都必須使用 **[清理市民]**。\n" +
                  "• 如果發生非預期行為，請還原原始城市存檔。" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "將診斷報告寫入記錄檔" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "- 寫入一份整理過的報告，其中包含 **25 個異常市民 ID**，以及各 **10 個搬離中、通勤者與無家可歸者 ID**（Index:Version）。\n\n" +
                  "- 也包含遊戲/CC 市民計數比較及私人汽車/自行車狀態。\n\n" +
                  "- **唯讀** — 不會刪除任何項目。\n\n" +
                  "- 記錄檔位置：\n" +
                  "%USERPROFILE%/AppData/LocalLow/Colossal Order/Cities Skylines II/logs/CitizenCleaner.log" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "一個按鈕即可寫入完整且易讀的疑難排解報告。不會刪除任何項目。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "開啟記錄檔" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)), "使用預設文字編輯器開啟記錄檔。" },
            };
        }

        public void Unload()
        {
        }
    }
}
