// LocaleJA.cs
using System.Collections.Generic;  // Dictionary
using Colossal;                    // IDictionarySource

namespace CitizenCleaner
{
    /// <summary>
    /// Japanese locale entries (ja-JP)
    /// </summary>
    public class LocaleJA : IDictionarySource
    {
        private readonly CCSetting m_Setting;
        public LocaleJA(CCSetting setting) { m_Setting = setting; }

        public IEnumerable<KeyValuePair<string, string>> ReadEntries(
            IList<IDictionaryEntryError> errors, Dictionary<string, int> indexCounts)
        {
            return new Dictionary<string, string>
            {
                // Mod name in Options menu list
                { m_Setting.GetSettingsLocaleID(), Mod.Name },

                // Tabs
                { m_Setting.GetOptionTabLocaleID(CCSetting.kSection), "アクション" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.AboutTab), "概要" },
                { m_Setting.GetOptionTabLocaleID(CCSetting.DebugTab), "デバッグ" },

                // Groups
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kFiltersGroup), "クリーンアップ対象" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.kButtonGroup), "アクション" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "Citizen & Vehicle Status" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "情報" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "デバッグ" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 破損した市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "有効（既定）にすると、**破損**した市民をカウントしてクリーンアップします。\n" +
                  "PropertyRenter コンポーネントがなく（かつホームレス、通勤者、移転中ではない）住民が対象です。\n\n" +
                  "破損した市民は本 Mod の主要な対象です。都市内に多すぎると、時間の経過とともに不具合の原因になり得ます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)), "▪ 移転中 (Rent = 0)" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeMovingAwayNoPR)),
                  "有効にすると、現在 **移転中** で Rent = 0（= PropertyRenter コンポーネントなし）の市民をカウントしてクリーンアップします。\n\n" +
                  "PropertyRenter を持つ、または Rent > 0 の移転中市民は削除されません。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCommuters)), "▪ 通勤者" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCommuters)),
                  "有効にすると、**通勤者** の市民をカウントしてクリーンアップします。通勤者とは、この都市に居住していないが仕事のために通う市民を指します。\n\n" +
                  "通勤者は、以前は本市に居住していたがホームレス化により転出した場合もあります（ゲーム v1.2.5 で追加された仕様）。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeHomeless)), "▪ ホームレス" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeHomeless)),
                  "有効にすると、**ホームレス** の市民をカウントしてクリーンアップします。\n\n" +
                  "<注意>：ホームレスを削除すると未知の副作用を引き起こす可能性があります。" },

                // Buttons
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupEntitiesButton)), "市民をクリーンアップ" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "先にセーブ済みの都市を読み込んでください。\nPropertyRenter コンポーネントが存在しない世帯から市民を削除します。\n" +
                  "クリーンアップには、[ ✓ ] で選択した任意項目も含まれます。\n\n" +
                  "**注意**：これは回避策であり、他のデータが破損する可能性があります。まずセーブのバックアップを作成してください！" },

                // Warning (confirmation)
                { m_Setting.GetOptionWarningLocaleID(nameof(CCSetting.CleanupEntitiesButton)),
                  "オプションで選択した項目を永久に削除します。\n\nまずセーブをバックアップしてください！\n 続行しますか？" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.RefreshCountsButton)), "カウントを更新" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.RefreshCountsButton)),
                  "＜数値を取得するには、先にセーブ済みの都市を読み込んでください。＞\n" +
                  "すべてのエンティティ数を更新し、現状の統計を表示します。\n" +
                  "クリーンアップ後は、ゲームを一時停止せずにしばらく動かしてください。" },

                // Read-only diagnostic report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "診断レポートをログに出力" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "破損市民25件、転出中・通勤者・ホームレス各10件のIDと、市民数・車両状態を読みやすいレポートにします。\n\n" +
                  "**読み取り専用** — 何も削除しません。" },

                // Sentence UNDER the button (multiline text row)
                // LabelLocale = inline body under the button
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "1つのボタンで完全な診断レポートを出力します。何も削除しません。" },

                // Displays
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CleanupStatusDisplay)), "ステータス" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CleanupStatusDisplay)),
                  "クリーンアップ状況を表示します。実行中はライブ更新されます。実行中でない場合は [カウントを更新] を押して再計算してください。\n\n" +
                  "\"**Idle**\" = クリーンアップ未実行、または都市が未読み込み。\n" +
                  "\"**Nothing to clean**\" = 選択されたフィルターに一致する市民がいない（または既に削除済み）。\n" +
                  "\"**Complete**\" = 直近のクリーンアップが完了。フィルターを変更するか新たに実行するまで維持されます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.TotalCitizensDisplay)), "市民総数" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.TotalCitizensDisplay)),
                  "現在のシミュレーション内に存在する市民エンティティの総数。\n\n" +
                  "破損エンティティを含む可能性があるため、人口とは異なる場合があります。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "クリーン対象の市民：上の [ ✓ ] を選択" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.CorruptedCitizensDisplay)),
                  "「**クリーンアップ**」をクリックした際に削除される市民エンティティ数。\n\n" +
                  "選択したチェックボックス [ ✓ ] に基づきます。" },

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
                { "CitizenCleaner/Prompt/RefreshCounts", "[カウントを更新] をクリック" },
                { "CitizenCleaner/Prompt/NoCity", "都市が読み込まれていません" },
                { "CitizenCleaner/Prompt/Error",  "エラー" },
                { "CitizenCleaner/Status/Progress", "クリーンアップ進行中… {0}" },
                { "CitizenCleaner/Status/Cleaning", "クリーン中… {0}" },

                // About tab fields
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.NameText)), "Mod 名" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.NameText)), "この Mod の表示名。" },
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.VersionText)), "バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.VersionText)), "現在の Mod バージョン。" },

#if DEBUG
                // Only visible in DEBUG builds
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.InformationalVersionText)), "情報バージョン" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.InformationalVersionText)), "コミット ID 付きの Mod バージョン" },
#endif

                // About tab links (the three external link buttons)
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "この Mod の GitHub リポジトリ。ブラウザで開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Mod へのフィードバック用 Discord。ブラウザで開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox Mods のウェブサイト。ブラウザで開きます。" },

                // About tab --> Usage section header & blocks
                { m_Setting.GetOptionGroupLocaleID(CCSetting.UsageGroup), "使用方法" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageSteps)),
                  "1. ＜まずセーブファイルをバックアップ！＞\n" +
                  "2. ＜[カウントを更新] をクリックして現在の統計を表示＞\n" +
                  "3. [ ✓ ] ＜含める項目をチェックボックスで選択＞\n" +
                  "4. ＜[市民をクリーンアップ] をクリックしてエンティティを整理＞" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageSteps)), "" },

                // Notes block
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.UsageNotes)),
                  "注意：\n" +
                  "• 本 Mod は自動では動作しません。削除するたびに **[市民をクリーンアップ]** を使用してください。\n" +
                  "• 予期しない動作があれば、元のセーブに戻してください。" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.UsageNotes)), "" },
            };
        }

        public void Unload() { }
    }
}
