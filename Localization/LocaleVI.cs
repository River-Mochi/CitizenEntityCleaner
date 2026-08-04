// Localization/LocaleVI.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Vietnamese locale (vi-VN)
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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "TRẠNG THÁI" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "Thông tin" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "Gỡ lỗi" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ Công dân lỗi" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "Khi bật (mặc định), đếm công dân **bị lỗi**.\n" +
                  "Họ thuộc hộ gia đình không có PropertyRenter và không phải người vô gia cư, người đi làm, khách du lịch hoặc người đang chuyển đi.\n\n" +
                  "- **Xe bị bỏ lại:** công dân lỗi và xe bị bỏ lại là mục tiêu chính.\n" +
                  "- Khi không còn thành viên nào trong hộ, trò chơi sẽ xóa phương tiện cá nhân và giải phóng chỗ đỗ.\n" +
                  "- CC đánh dấu công dân để xóa; hệ thống dọn dẹp của trò chơi xử lý các tham chiếu đến xe, trường học, bệnh nhân và mục khác." },


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
                  "Đếm và dọn thành viên của **HomelessHousehold**.\n\n" +
                  "Xóa người vô gia cư làm thay đổi dân số và nhu cầu nhà ở.\n" +
                  "Nhiều hộ vô gia cư hơn làm giảm nhu cầu chung nhưng tăng yếu tố tích cực cho mật độ cao." },

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

                // Cleanup Status and Counts
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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "Nhà ở" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "Số hộ hiện tại; cập nhật bằng [Làm mới số liệu].\n" +
                  "<Đang tìm> = hộ có PropertySeeker được bật, gồm cả hộ vô gia cư.\n" +
                  "<Chuyển vào/đi> = bộ đếm hộ của trò chơi 1.6.\n" +
                  "PropertySeeker nghĩa là đang tìm, không phải tìm thất bại." },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "Ô tô" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "Chỉ ô tô cá nhân; xe thuộc nhóm xe đạp và rơ-moóc được báo cáo riêng.\n" +
                  "<Hoạt động> = đang ở trên làn và không đỗ; có thể đang chạy hoặc dừng.\n" +
                  "<Đang đỗ> = tất cả ô tô cá nhân đang đỗ.\n" +
                  "<Tổng> = ô tô cá nhân đang hoạt động, đang đỗ và chuyển trạng thái.\n" +
                  "<Cập nhật> = thời gian làm mới các số liệu.\n\n" +
                  "Mô phỏng thành phố tạm dừng trong Tùy chọn. Hãy chạy thành phố trước khi làm mới." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "Ô tô đang đỗ" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<Đường phố> = ô tô hiển thị đang đỗ trên ParkingLane của đường.\n" +
                  "<Cơ sở> = ô tô trong tòa nhà, ga-ra hoặc cơ sở đỗ xe.\n" +
                  "<OC> = ô tô bị ẩn tại kết nối bên ngoài.\n" +
                  "<Khác> = ô tô đang đỗ không khớp các mục trên; một số không có làn đỗ xe được gán.\n" +
                  "<Không có làn> không tự nó có nghĩa là xe bị bỏ lại.\n\n" +
                  "Dùng **[BÁO CÁO NHẬT KÝ]**, sau đó **[MỞ NHẬT KÝ]**, để xem chi tiết và ID thực thể." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "Ô tô tại OC" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "Ô tô bị ẩn tại kết nối bên ngoài, được nhóm theo chủ sở hữu.\n" +
                  "<Thành phố> = chủ sở hữu là hộ gia đình trong thành phố.\n" +
                  "<Tại OC> = hộ sở hữu hiện đang ở một OC.\n" +
                  "<Chủ OC> = thường là DummyTraffic do trò chơi tạo, không phải xe của cư dân.\n" +
                  "<Bên ngoài> = hộ người đi làm, khách du lịch hoặc đang chuyển đi.\n" +
                  "<Thiếu> = không có chủ hoặc chủ không phải hộ gia đình." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "BÁO CÁO NHẬT KÝ" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "Ghi số lượng công dân và phương tiện cùng các **ID thực thể** mẫu vào CitizenCleaner.log.\n" +
                  "Sao chép ID vào mod **Scene Explorer** để kiểm tra." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "MỞ NHẬT KÝ" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "Mở **CitizenCleaner.log**." },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "Bấm [Làm mới số liệu]" },
                { "CitizenCleaner/Prompt/NoCity", "Chưa mở thành phố" },
                { "CitizenCleaner/Prompt/Error",  "Lỗi" },
                { "CitizenCleaner/Status/Progress", "Đang dọn dẹp… {0}" },
                { "CitizenCleaner/Status/Cleaning", "Đang dọn… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} đang tìm | {1} đang chuyển vào | {2} đang chuyển đi" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} hoạt động | {1} đang đỗ | {2} tổng | cập nhật {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} đường phố | {1} cơ sở | {2} OC | {3} khác" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2",
                  "{0} thành phố | {1} tại OC | {2} chủ OC | {3} bên ngoài | {4} thiếu" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — BÁO CÁO NHẬT KÝ\n" +
                  "Tạo lúc: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[ĐỐI CHIẾU SỐ CÔNG DÂN — TRÒ CHƠI 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "Bộ đếm của trò chơi 1.6 chỉ dùng để chẩn đoán; CC dùng số dọn dẹp riêng.\n" +
                  "ValidCitizen là cờ dân số đã chuyển vào, không phải phép kiểm tra công dân lỗi của CC.\n" +
                  "Trò chơi đếm hộ đang chuyển đi và hộ người đi làm; CC đếm công dân." },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[ĐỐI CHIẾU DÂN SỐ VÔ GIA CƯ]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[TRẠNG THÁI NHÀ Ở CỦA HỘ]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "Các trạng thái này có thể chồng lên nhau. PropertySeeker nghĩa là đang tìm, không phải thất bại. Quy tắc công dân hỏng hiện tại của CC không loại trừ họ." },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "Hộ không có PropertyRenter và chưa MovedIn" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "Hộ không có PropertyRenter và đã bật PropertySeeker" },
                { "CitizenCleaner/Report/GameCountsPending", "Số liệu trò chơi vẫn đang khởi tạo." },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[ID THỰC THỂ CÔNG DÂN — dùng Scene Explorer; Chỉ_mục:Phiên_bản]" },
                { "CitizenCleaner/Report/CorruptCitizens", "Công dân lỗi" },
                { "CitizenCleaner/Report/MovingAwayCitizens",
                  "Công dân đang chuyển đi (hộ có MovingAway + không có PropertyRenter)" },
                { "CitizenCleaner/Report/CommuterCitizens", "Công dân đi làm" },
                { "CitizenCleaner/Report/HomelessCitizens", "Ứng viên vô gia cư để dọn" },
                { "CitizenCleaner/Report/IdsLabel", "ID: " },
                { "CitizenCleaner/Report/None", "(không có)" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[PHƯƠNG TIỆN CÁ NHÂN]\n" +
                  "Không có snapshot phương tiện.\n" },


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
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Trang Paradox Mods; mở trong trình duyệt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "Kho GitHub của mod; mở trong trình duyệt." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Discord để góp ý về mod; mở trong trình duyệt." },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "Ghi ID thực thể" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "Ghi mẫu **25 công dân lỗi**, **10 đang chuyển đi, 10 người đi làm và 10 người vô gia cư**.\n" +
                  "Cũng ghi ID thực thể của phương tiện đáng ngờ.\n" +
                  "Dùng mod **Scene Explorer** để kiểm tra một ID." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "Dùng [Ghi ID thực thể], [Mở nhật ký], rồi sao chép ID thực thể vào mod Scene Explorer khi đang ở trong thành phố." },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "Mở nhật ký" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "Mở **Logs/CitizenCleaner.log** hoặc thư mục Logs nếu không có tệp." },

            };
        }
        public void Unload() { }
    }
}
