// LocaleVI.cs
using System.Collections.Generic;  // Dictionary
using Colossal;                    // IDictionarySource

namespace CitizenCleaner
{
    /// <summary>
    /// Vietnamese locale entries (vi-VN)
    /// </summary>
    public class LocaleVI : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleVI(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "Thao tác" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "About" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "Gỡ lỗi" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "Mục tiêu dọn dẹp" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "Thao tác" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Thông tin" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Gỡ lỗi" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Công dân lỗi" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Khi bật (mặc định), sẽ đếm và dọn **công dân lỗi**;\n" +
                  "cư dân thiếu component PropertyRenter (và không phải vô gia cư, commuter, khách du lịch, hay đang rời đi).\n\n" +
                  "Công dân lỗi là mục tiêu chính của mod. Nếu thành phố có quá nhiều, lâu dần có thể gây vấn đề." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ Bỏ đi (Tiền thuê = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "Khi bật, sẽ đếm và dọn các công dân đang **Bỏ đi** với Tiền thuê = 0 (tức là không có component PropertyRenter).\n\n" +
                  "Công dân Bỏ đi có PropertyRenter hoặc Tiền thuê > 0 sẽ không bị xóa." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ Người đi làm (commuter)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "Khi bật, sẽ đếm và dọn **commuter**. Commuter là người không sống trong thành phố của bạn nhưng vào thành phố để làm việc.\n\n" +
                  "Đôi khi, commuter từng sống trong thành phố nhưng đã chuyển đi vì vô gia cư (tính năng thêm từ phiên bản game 1.2.5)." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ Vô gia cư" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "Khi bật, sẽ đếm và dọn **người vô gia cư**.\n\n" +
                  "<CẨN THẬN>: xóa người vô gia cư có thể gây tác dụng phụ khó lường." },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "Dọn công dân" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Hãy mở thành phố đã lưu trước.\nXóa công dân khỏi các hộ gia đình không còn component PropertyRenter.\n" +
                  "Dọn dẹp cũng bao gồm các mục tùy chọn đã chọn [ ✓ ].\n\n" +
                  "**CẨN THẬN**: đây là cách tạm thời và có thể làm hỏng dữ liệu khác. Hãy sao lưu save trước!" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "Xóa vĩnh viễn các mục đã chọn trong tùy chọn.\n\nHãy sao lưu save trước!\n Tiếp tục?" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "Làm mới số liệu" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "<Hãy mở thành phố đã lưu để có số liệu.>\n" +
                  "Cập nhật toàn bộ số đếm để hiển thị thống kê hiện tại của thành phố.\n" +
                  "Sau khi dọn, cho game chạy (không tạm dừng) khoảng một phút." },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Ghi báo cáo chẩn đoán" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Ghi báo cáo dễ đọc: 25 ID công dân lỗi và 10 ID cho mỗi nhóm đang chuyển đi, đi làm và vô gia cư; cùng số liệu công dân và xe.\n\n" +
                  "**Chỉ đọc** — không xóa gì." },

                // Sentence UNDER the button (multiline text row)
                // LabelLocale = inline body under the button
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Một nút ghi toàn bộ báo cáo chẩn đoán. Không xóa gì." },

                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "Trạng thái" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "Hiển thị trạng thái dọn dẹp. Cập nhật trực tiếp khi đang dọn; nếu không, bấm [Làm mới số liệu] để tính lại.\n\n" +
                  "\"**Nghỉ**\" = chưa chạy dọn hoặc chưa mở city.\n" +
                  "\"**Không có gì để dọn**\" = không có công dân khớp bộ lọc đã chọn (hoặc bạn đã xóa hết).\n" +
                  "\"**Xong**\" = lần dọn trước đã hoàn tất; giữ nguyên cho đến khi đổi bộ lọc hoặc chạy dọn mới." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "Tổng công dân" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "Tổng số thực thể công dân **đang có trong mô phỏng.**\n\n" +
                  "Con số này có thể khác dân số vì có thể gồm cả thực thể lỗi." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Công dân sẽ dọn: chọn [ ✓ ] ở trên" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "Số công dân sẽ bị xóa khi bấm **[Dọn công dân]**,\n\n" +
                  "dựa trên các ô đã chọn [ ✓ ]." },

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
                { "CitizenCleaner/Prompt/RefreshCounts", "Bấm [Làm mới số liệu]" },
                { "CitizenCleaner/Prompt/NoCity", "Chưa mở thành phố" },
                { "CitizenCleaner/Prompt/Error",  "Lỗi" },
                { "CitizenCleaner/Status/Progress", "Đang dọn dẹp… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Đang dọn… {0}" },

                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Tên mod" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "Tên hiển thị của mod." },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "Phiên bản" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "Phiên bản mod hiện tại." },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "Phiên bản thông tin" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "Phiên bản mod kèm Commit ID" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Kho GitHub của mod; mở trong trình duyệt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord để góp ý về mod; mở trong trình duyệt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Trang Paradox Mods; mở trong trình duyệt." },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "CÁCH DÙNG" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. <Hãy sao lưu save trước!>\n" +
                  "2. <Bấm [Làm mới số liệu] để xem thống kê hiện tại.>\n" +
                  "3. [ ✓ ] <Đánh dấu những mục muốn xử lý>\n" +
                  "4. <Bấm [Dọn công dân] để dọn thực thể.>" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "Ghi chú:\n" +
                  "• Mod **không** chạy tự động; hãy dùng **[Dọn công dân]** mỗi lần muốn xóa.\n" +
                  "• Có thể quay lại save gốc nếu gặp hành vi bất thường." },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}
