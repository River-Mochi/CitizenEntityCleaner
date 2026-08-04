// Localization/LocaleZH_HANS.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Simplified Chinese locale (zh-HANS)
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
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "操作" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "关于" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "调试" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "清理目标" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "操作" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "状态" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "信息" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "调试" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 损坏的市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "启用时（默认），统计**损坏的**市民。\n" +
                  "这些市民属于没有 PropertyRenter 的家庭，并且不是无家可归者、通勤者、游客或正在搬离者。\n\n" +
                  "- **废弃汽车：**损坏的市民和废弃汽车是主要清理目标。\n" +
                  "- 当家庭中没有剩余成员时，游戏应移除其私人车辆并释放停车位。\n" +
                  "- CC 将市民标记为删除；游戏的清理系统会处理车辆、学校、患者及其他引用。" },


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
                  "统计并清理 **HomelessHousehold** 的成员。\n\n" +
                  "删除无家可归者会改变人口和住宅需求。\n" +
                  "无家可归家庭越多，整体需求越低，但高密度住宅的正向需求因素越高。" },

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

                // Cleanup Status and Counts
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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "住房" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "当前家庭数量；使用 [刷新计数] 更新。\n" +
                  "<正在找房> = 已启用 PropertySeeker 的家庭，包括无家可归家庭。\n" +
                  "<正在迁入/迁出> = 游戏 1.6 的家庭计数器。\n" +
                  "PropertySeeker 表示正在寻找，并不表示寻找失败。" },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "汽车" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "仅统计私人汽车；自行车组车辆和拖车单独报告。\n" +
                  "<活动> = 位于车道上且未停放；可能正在行驶或停车等待。\n" +
                  "<已停放> = 所有已停放的私人汽车。\n" +
                  "<总计> = 活动、已停放和过渡状态的私人汽车。\n" +
                  "<更新> = 这些数量的刷新时间。\n\n" +
                  "打开选项时城市模拟会暂停。刷新前请先运行城市以查看变化。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "已停放汽车" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<街道> = 在街道 ParkingLane 上可见的已停放汽车。\n" +
                  "<设施> = 位于建筑、车库或停车设施内的汽车。\n" +
                  "<OC> = 位于外部连接且被隐藏的汽车。\n" +
                  "<其他> = 不符合上述位置的已停放汽车；部分没有分配停车车道。\n" +
                  "<无车道> 本身并不表示汽车已被废弃。\n\n" +
                  "使用 **[日志报告]**，然后使用 **[打开日志]** 查看详细信息和实体 ID。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC 汽车" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "位于外部连接且被隐藏的汽车，按所有者分组。\n" +
                  "<城市> = 所有者是城市家庭。\n" +
                  "<位于 OC> = 所有者家庭当前位于 OC。\n" +
                  "<OC 所有者> = 通常是游戏生成的 DummyTraffic，不是居民汽车。\n" +
                  "<外来> = 通勤、游客或正在搬离的家庭。\n" +
                  "<缺失> = 没有所有者，或所有者不是家庭。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "日志报告" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "将市民和车辆数量以及示例**实体 ID** 写入 CitizenCleaner.log。\n" +
                  "将 ID 复制到 **Scene Explorer** 模组中进行检查。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "打开日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "打开 **CitizenCleaner.log**。" },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "点击 [刷新计数]" },
                { "CitizenCleaner/Prompt/NoCity", "未加载城市" },
                { "CitizenCleaner/Prompt/Error",  "错误" },
                { "CitizenCleaner/Status/Progress", "正在清理… {0}" },
                { "CitizenCleaner/Status/Cleaning", "清理中… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} 正在找房 | {1} 正在迁入 | {2} 正在迁出" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} 活动 | {1} 已停放 | {2} 总计 | 更新 {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} 街道 | {1} 设施 | {2} OC | {3} 其他" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2", "{0} 城市 | {1} 位于 OC | {2} OC 所有者 | {3} 外来 | {4} 缺失" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — 日志报告\n" +
                  "生成时间：{0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[市民数量交叉检查 — 游戏 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "游戏 1.6 计数器仅用于诊断；CC 使用自己的清理数量。\n" +
                  "ValidCitizen 是已迁入人口标记，不是 CC 的损坏市民判定。\n" +
                  "游戏的搬离和通勤数值统计家庭；CC 统计市民。" },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[无家可归人口交叉检查]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[家庭住房状态]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "这些当前状态可能重叠。PropertySeeker 表示正在寻找，不表示失败。CC 当前的异常市民规则不会排除他们。" },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "没有 PropertyRenter 且未 MovedIn 的家庭" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "没有 PropertyRenter 且已启用 PropertySeeker 的家庭" },
                { "CitizenCleaner/Report/GameCountsPending", "游戏计数仍在初始化。" },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[市民实体 ID — 使用 Scene Explorer；索引:版本]" },
                { "CitizenCleaner/Report/CorruptCitizens", "损坏的市民" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "正在搬离的市民（家庭 MovingAway + 无 PropertyRenter）" },
                { "CitizenCleaner/Report/CommuterCitizens", "通勤市民" },
                { "CitizenCleaner/Report/HomelessCitizens", "无家可归清理候选市民" },
                { "CitizenCleaner/Report/IdsLabel", "ID：" },
                { "CitizenCleaner/Report/None", "（无）" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[私人车辆]\n" +
                  "车辆快照不可用。\n" },


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
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "打开浏览器访问 Paradox Mods 页面。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "打开浏览器访问本模组的 GitHub 仓库。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "打开浏览器加入模组反馈的 Discord 频道。" },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "记录实体 ID" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "记录 **25 个损坏市民**、**10 个正在搬离、10 个通勤和 10 个无家可归市民**的示例。\n" +
                  "还会记录可疑车辆的实体 ID。\n" +
                  "使用 **Scene Explorer** 模组检查 ID。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "依次使用[记录实体 ID]、[打开日志]，然后在城市中将实体 ID 复制到 Scene Explorer 模组。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "打开日志" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "打开 **Logs/CitizenCleaner.log**；如果文件不可用，则打开 Logs 文件夹。" },

            };
        }
        public void Unload() { }
    }
}
