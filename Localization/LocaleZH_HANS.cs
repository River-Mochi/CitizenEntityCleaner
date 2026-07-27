// LocaleZH_HANS.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary

    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Simplified Chinese (zh-CN) locale
    /// </summary>
    public class LocaleZH_HANS : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleZH_HANS(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list (keep Mod.Name so display stays consistent)
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "操作" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "关于" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "调试" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "清理目标" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "操作" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "信息" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "调试" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 损坏的市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "启用（默认）后，将统计并清理**损坏的**市民；\n" +
                  "即缺少 PropertyRenter 组件且不是无家可归者、通勤者、游客或搬离中的常住居民。\n\n" +
                  "损坏的市民是本模组的主要清理对象；数量过多会随时间造成问题。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ 搬离中（租金 = 0）" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "启用后，将统计并清理当前**搬离中**且租金为 0 的市民（即无 PropertyRenter 组件）。\n\n" +
                  "拥有 PropertyRenter 或租金 > 0 的搬离中市民不会被移除。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ 通勤者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "启用后，将统计并清理**通勤者**。通勤者是不居住在你的城市、仅为工作而来往的市民。\n\n" +
                  "有时通勤者曾居住在你的城市，但因无家可归而搬离（游戏 1.2.5 版本新增的行为）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ 无家可归者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "启用后，将统计并清理**无家可归者**。\n\n" +
                  "<注意>：删除无家可归者可能产生未知的副作用。" },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "清理市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "<请先加载存档。>\n从不再拥有 PropertyRenter 组件的家庭中移除市民。\n" +
                  "清理同时包含你勾选的可选项 [ ✓ ]。\n\n" +
                  "**请注意**：这是权宜之计，可能损坏其他数据。请先备份你的存档！" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "将永久删除在选项中勾选的项目。\n\n<请先备份你的存档！>\n是否继续？" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "刷新计数" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<要获取数据，请先加载存档。>\n" +
                  "更新所有实体计数以显示当前城市统计。\n" +
                  "清理后请让游戏继续运行一段时间。" },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "将诊断报告写入日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "写入易读报告：25 个损坏市民 ID，以及搬离中、通勤者和无家可归者各 10 个 ID；还包括市民计数和车辆状态。\n\n" +
                  "**只读** — 不会删除任何内容。" },


                // Sentence UNDER the button (multiline)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "一个按钮写入完整诊断报告。不会删除任何内容。" },


                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "状态" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "显示清理状态。在清理进行中会实时更新；否则请点击 [刷新计数] 重新计算。\n\n" +
                  "“**Idle**” = 未在清理或尚未加载城市。\n" +
                  "“**Nothing to clean**” = 没有市民符合所选过滤条件（或你已清理完毕）。\n" +
                  "“**Complete**” = 上次清理已结束；直到你修改过滤条件或再次运行清理前保持该状态。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "市民总数" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "当前**参与模拟**的市民实体总数。\n\n" +
                  "该数字可能与人口数不同，因为其中可能包含损坏的实体。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "待清理的市民：请在上方勾选 [ ✓ ]" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "当你点击 **[清理]** 时将要移除的市民数量，\n\n" +
                  "基于你在上方勾选的选项 [ ✓ ]。" },

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
                { "CitizenCleaner/Prompt/RefreshCounts", "点击 [刷新计数]" },
                { "CitizenCleaner/Prompt/NoCity", "未加载城市" },
                { "CitizenCleaner/Prompt/Error",  "错误" },
                { "CitizenCleaner/Status/Progress", "正在清理… {0}" },
                { "CitizenCleaner/Status/Cleaning", "清理中… {0}" },

                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "模组名称" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "本模组的显示名称。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "当前模组版本。" },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "信息版本" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "包含提交 ID 的版本号" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "打开浏览器访问本模组的 GitHub 仓库。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "打开浏览器加入模组反馈的 Discord 频道。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "打开浏览器访问 Paradox Mods 页面。" },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "用法" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <请先备份你的存档！>\n" +
                  "2. <点击 [刷新计数] 查看当前统计。>\n" +
                  "3. <使用复选框勾选> [ ✓ ] <要包含的项目>\n" +
                  "4. <点击 [清理市民] 执行清理。>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "注意：\n" +
                  "• 本模组**不会**自动运行；每次需要移除时请手动点击 **[清理市民]**。\n" +
                  "• 如出现异常行为，请还原到原始存档。" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}

