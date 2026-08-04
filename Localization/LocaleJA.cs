// Localization/LocaleJA.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;  // Dictionary
    using Colossal;                    // IDictionarySource

    /// <summary>
    /// Japanese locale (ja-JP)
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
                { m_Setting.GetOptionGroupLocaleID(CCSetting.StatusGroup), "ステータス" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.InfoGroup), "情報" },
                { m_Setting.GetOptionGroupLocaleID(CCSetting.DebugGroup), "デバッグ" },

                // Filter toggles
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.IncludeCorrupt)), "▪ 破損した市民" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.IncludeCorrupt)),
                  "有効（既定）の場合、**破損した**市民を数えます。\n" +
                  "PropertyRenter のない世帯に属し、ホームレス、通勤者、観光客、転出中の市民ではありません。\n\n" +
                  "- **放置車両:** 破損した市民と放置車両が主な対象です。\n" +
                  "- 世帯員が誰も残っていない場合、ゲームは個人車両を削除して駐車場所を空けるはずです。\n" +
                  "- CC は市民を削除対象にし、ゲームのクリーンアップシステムが車両、学校、患者などの参照を処理します。" },


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
                  "**HomelessHousehold** のメンバーを数えて整理します。\n\n" +
                  "ホームレスを削除すると、人口と住宅需要が変化します。\n" +
                  "ホームレス世帯が増えると全体需要は下がりますが、高密度住宅へのプラス要因は増えます。" },

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

                // Cleanup Status and Counts
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

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHousing)), "住宅" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHousing)),
                  "現在の世帯数です。[カウント更新] で更新されます。\n" +
                  "<住宅検索中> = PropertySeeker が有効な世帯。ホームレス世帯も含みます。\n" +
                  "<転入中/転出中> = ゲーム 1.6 の世帯カウンターです。\n" +
                  "PropertySeeker は現在検索中という意味で、検索失敗ではありません。" },

                // Status Cars
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusCars)), "自動車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusCars)),
                  "個人所有の自動車のみ。自転車グループの車両とトレーラーは別に報告されます。\n" +
                  "<走行中> = レーン上にあり、駐車中ではありません。走行または停止している場合があります。\n" +
                  "<駐車中> = 駐車中のすべての個人所有車。\n" +
                  "<合計> = 走行中、駐車中、移行中の個人所有車。\n" +
                  "<更新> = カウントを更新した時刻。\n\n" +
                  "オプション画面では都市シミュレーションが一時停止します。変化を見るには都市を動かしてから更新してください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusParkedCars)), "駐車中の自動車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusParkedCars)),
                  "<路上> = 道路の ParkingLane にある表示中の駐車車両。\n" +
                  "<施設> = 建物、ガレージ、駐車施設内の自動車。\n" +
                  "<OC> = 外部接続にある非表示の自動車。\n" +
                  "<その他> = 上記に一致しない駐車車両。一部は駐車レーンが割り当てられていません。\n" +
                  "<レーンなし> だけでは放置車両とは判断できません。\n\n" +
                  "詳細とエンティティ ID は **[ログレポート]**、続いて **[ログを開く]** を使用してください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.StatusHiddenAtOc)), "OC の自動車" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.StatusHiddenAtOc)),
                  "外部接続にある非表示の自動車を所有者別に表示します。\n" +
                  "<都市> = 所有者が都市内の世帯。\n" +
                  "<OC に所在> = 所有世帯が現在 OC にいる。\n" +
                  "<OC 所有者> = 通常はゲームが生成した DummyTraffic で、住民の車ではありません。\n" +
                  "<市外> = 通勤者、観光客、または転出中の世帯。\n" +
                  "<不明> = 所有者なし、または所有者が世帯ではありません。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogStatusReportButton)), "ログレポート" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogStatusReportButton)),
                  "CitizenCleaner.log に市民・車両数と **エンティティ ID** の例を書き込みます。\n" +
                  "ID を **Scene Explorer** Mod にコピーして確認できます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "ログを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogFromStatusButton)), "**CitizenCleaner.log** を開きます。" },

                // Prompts (used by CCSetting.cs for placeholder text)
                { "CitizenCleaner/Prompt/RefreshCounts", "[カウントを更新] をクリック" },
                { "CitizenCleaner/Prompt/NoCity", "都市が読み込まれていません" },
                { "CitizenCleaner/Prompt/Error",  "エラー" },
                { "CitizenCleaner/Status/Progress", "クリーンアップ進行中… {0}" },
                { "CitizenCleaner/Status/Cleaning", "クリーン中… {0}" },
                { "CitizenCleaner/Status/HousingRowV1", "{0} 住宅検索中 | {1} 転入中 | {2} 転出中" },
                { "CitizenCleaner/Status/CarSummaryRowV2", "{0} 走行中 | {1} 駐車中 | {2} 合計 | 更新 {3}" },
                { "CitizenCleaner/Status/CarParkingRowV2", "{0} 路上 | {1} 施設 | {2} OC | {3} その他" },
                { "CitizenCleaner/Status/OcHiddenOwnerRowV2", "{0} 都市 | {1} OC に所在 | {2} OC 所有者 | {3} 市外 | {4} 不明" },

                // Diagnostic report
                { "CitizenCleaner/Report/Header",
                  "CITIZEN CLEANER — ログレポート\n" +
                  "生成: {0}" },
                { "CitizenCleaner/Report/CitizenCrossCheckHeading", "[市民数の照合 — ゲーム 1.6]" },

                { "CitizenCleaner/Report/CitizenCrossCheckNote",
                  "ゲーム 1.6 のカウンターは診断用です。CC は独自のクリーンアップ数を使用します。\n" +
                  "ValidCitizen は転入済み人口のフラグであり、CC の破損市民判定ではありません。\n" +
                  "ゲームの転出中と通勤者の値は世帯数、CC は市民数です。" },

                { "CitizenCleaner/Report/HomelessCheckHeading", "[ホームレス人口の照合]" },
                { "CitizenCleaner/Report/HouseholdHousingHeading", "[世帯の住居状態]" },
                { "CitizenCleaner/Report/HouseholdHousingNote",
                  "現在の状態は重複する場合があります。PropertySeeker は検索中を意味し、失敗ではありません。CC の現行の破損市民ルールでは除外されません。" },
                { "CitizenCleaner/Report/NoRenterNotMovedInHouseholds",
                  "PropertyRenter なし、かつ MovedIn でない世帯" },
                { "CitizenCleaner/Report/NoRenterPropertySeekerHouseholds",
                  "PropertyRenter なし、かつ PropertySeeker が有効な世帯" },
                { "CitizenCleaner/Report/GameCountsPending", "ゲームのカウントを初期化中です。" },
                { "CitizenCleaner/Report/CitizenIdsHeading", "[市民エンティティ ID — Scene Explorer を使用; Index:Version]" },
                { "CitizenCleaner/Report/CorruptCitizens", "破損した市民" },
                { "CitizenCleaner/Report/MovingAwayCitizens", "転出中の市民（世帯に MovingAway + PropertyRenter なし）" },
                { "CitizenCleaner/Report/CommuterCitizens", "通勤者" },
                { "CitizenCleaner/Report/HomelessCitizens", "整理対象のホームレス市民" },
                { "CitizenCleaner/Report/IdsLabel", "ID: " },
                { "CitizenCleaner/Report/None", "（なし）" },
                { "CitizenCleaner/Report/VehicleSnapshotUnavailable",
                  "[個人車両]\n" +
                  "車両スナップショットを利用できません。\n" },


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
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenParadoxModsButton)), "Paradox Mods" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenParadoxModsButton)),  "Paradox Mods のウェブサイト。ブラウザで開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenGithubButton)),  "GitHub" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenGithubButton)),   "この Mod の GitHub リポジトリ。ブラウザで開きます。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenDiscordButton)), "Discord" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenDiscordButton)),  "Mod へのフィードバック用 Discord。ブラウザで開きます。" },
               
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


                 // Debug report
                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.LogDiagnosticReportButton)), "エンティティ ID をログ出力" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.LogDiagnosticReportButton)),
                  "**破損 25 人**、**転出中 10 人、通勤者 10 人、ホームレス 10 人**の例をログに出力します。\n" +
                  "疑わしい車両のエンティティ ID も出力します。\n" +
                  "ID の確認には **Scene Explorer** Mod を使用してください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.DebugReportNote)),
                  "[エンティティ ID をログ出力]、[ログを開く] の順に押し、都市内で ID を Scene Explorer Mod にコピーしてください。" },

                { m_Setting.GetOptionLabelLocaleID(nameof(CCSetting.OpenLogButton)), "ログを開く" },
                { m_Setting.GetOptionDescLocaleID(nameof(CCSetting.OpenLogButton)),
                  "**Logs/CitizenCleaner.log** を開きます。ファイルがない場合は Logs フォルダーを開きます。" },

            };
        }
        public void Unload() { }
    }
}
