// File: CitizenCleanupSystem.Debug.Citizens.cs
namespace CitizenCleaner
{
    using System.Collections.Generic;
    using System.Text;
    using Game.Agents;        // MovingAway
    using Game.Buildings;
    using Game.Citizens;
    using Unity.Collections;
    using Unity.Entities;

    public partial class CitizenCleanupSystem
    {
        private static void AppendCitizenCrossCheck(
            StringBuilder report,
            DiagnosticCitizenCounts cc,
            CitizenCountSnapshot game)
        {
            report.AppendLine(ReportText(
                "CitizenCrossCheckHeading",
                "[CITIZEN COUNT CROSS-CHECK — GAME 1.6]"));
            report.AppendLine(
                $"CC total: {game.CCHouseholdMemberEntities:N0} " +
                "household-member entities");
            report.AppendLine(
                $"Game total: {game.GameValidMovedInCitizens:N0} " +
                "valid moved-in citizens");
            report.AppendLine();
            report.AppendLine(
                $"Homeless: CC {cc.HomelessHouseholdMembers:N0} cleanup candidates | " +
                $"game {game.GameHomelessCitizens:N0} valid citizens");
            report.AppendLine(
                $"Moving-away: CC {cc.MovingAway:N0} citizens | " +
                $"game {game.GameMovingAwayHouseholds:N0} households");
            report.AppendLine(
                $"Commuters: CC {cc.Commuters:N0} citizens | " +
                $"game {game.GameCommuterHouseholds:N0} households");
            report.AppendLine(
                $"Tourists: game {game.GameTouristCitizens:N0} citizens");

            report.AppendLine(ReportText(
                "CitizenCrossCheckNote",
                "Game 1.6 counters are diagnostic only; " +
                "CC uses its own cleanup count.\n" +
                "ValidCitizen is a moved-in population flag, " +
                "not CC's corrupt-citizen test.\n" +
                "Game moving-away and commuter counts are households; " +
                "CC counts citizens."));

            if (!game.GameCountsReady)
            {
                report.AppendLine(ReportText(
                    "GameCountsPending",
                    "Game counts are still initializing."));
            }

            report.AppendLine();
        }

        private static void AppendHomelessDiagnostics(
            StringBuilder report,
            DiagnosticCitizenCounts counts,
            List<string> mismatchDetails)
        {
            int outsideGameCount =
                counts.HomelessHouseholdMembers - counts.HomelessValid;

            report.AppendLine(ReportText(
                "HomelessCheckHeading",
                "[HOMELESS POPULATION CROSS-CHECK]"));
            report.AppendLine(
                $"{counts.HomelessHouseholdMembers:N0} cleanup candidates | " +
                $"{counts.HomelessValid:N0} game-valid | " +
                $"{outsideGameCount:N0} outside the game-valid count");
            report.AppendLine(
                $"Outside game-valid count: {counts.HomelessDead:N0} dead | " +
                $"{counts.HomelessMissingValidCitizen:N0} missing ValidCitizen | " +
                $"{counts.HomelessCommuter:N0} commuter | " +
                $"{counts.HomelessTourist:N0} tourist | " +
                $"{counts.HomelessMissingFlag:N0} missing Homeless flag | " +
                $"{counts.HomelessMissingCitizen:N0} missing Citizen");
            report.AppendLine(
                $"Missing ValidCitizen members: " +
                $"{counts.HomelessMissingValidMovingAway:N0} moving-away | " +
                $"{counts.HomelessMissingValidNotMovedIn:N0} not moved-in | " +
                $"{counts.HomelessMissingValidMovedInMismatch:N0} " +
                "moved-in mismatch");

            report.AppendLine(
                "Moving-away is expected. Recheck not moved-in IDs after running the city; " +
                "inspect moved-in mismatches in Scene Explorer.");
            report.AppendLine(
                "CC keeps its existing broad HomelessHousehold cleanup rule; " +
                "these exclusions are diagnostic only.");
            report.AppendLine("Game-count exclusion samples:");

            if (mismatchDetails.Count == 0)
            {
                report.AppendLine("  " + ReportText("None", "(none)"));
            }
            else
            {
                for (int i = 0; i < mismatchDetails.Count; i++)
                    report.AppendLine("  " + mismatchDetails[i]);
            }

            report.AppendLine();
        }

        private static void AppendHouseholdHousingDiagnostics(
            StringBuilder report,
            DiagnosticCitizenCounts counts,
            CitizenCountSnapshot game,
            NativeList<Entity> notMovedInWithoutProperty,
            NativeList<Entity> propertySeekersWithoutProperty)
        {
            report.AppendLine(ReportText(
                "HouseholdHousingHeading",
                "[HOUSEHOLD HOUSING STATES]"));
            report.AppendLine(
                $"Game moving in: {game.GameMovingInHouseholds:N0} households | " +
                $"moving away: {game.GameMovingAwayHouseholds:N0} households");
            report.AppendLine(
                $"PropertySeeker enabled: " +
                $"{counts.PropertySeekerHouseholds:N0} total | " +
                $"{counts.NormalPropertySeekerHouseholds:N0} normal | " +
                $"{counts.HomelessPropertySeekerHouseholds:N0} homeless households");
            report.AppendLine(
                $"No PropertyRenter + not MovedIn: " +
                $"{counts.NoPropertyRenterNotMovedInHouseholds:N0} households");
            report.AppendLine(
                $"No PropertyRenter + PropertySeeker enabled: " +
                $"{counts.NoPropertyRenterPropertySeekerHouseholds:N0} households");
            report.AppendLine(
                $"Current corrupt candidates: {counts.Corrupt:N0} total | " +
                $"{counts.CorruptNotMovedInCitizens:N0} not MovedIn | " +
                $"{counts.CorruptPropertySeekerCitizens:N0} PropertySeeker enabled");
            report.AppendLine(ReportText(
                "HouseholdHousingNote",
                "These current states can overlap. PropertySeeker means searching, " +
                "not failed. CC's current corrupt rule does not exclude them."));
            report.AppendLine();

            AppendEntitySection(
                report,
                ReportText(
                    "NoRenterNotMovedInHouseholds",
                    "Households with no PropertyRenter and not MovedIn"),
                counts.NoPropertyRenterNotMovedInHouseholds,
                notMovedInWithoutProperty);
            AppendEntitySection(
                report,
                ReportText(
                    "NoRenterPropertySeekerHouseholds",
                    "Households with no PropertyRenter and PropertySeeker enabled"),
                counts.NoPropertyRenterPropertySeekerHouseholds,
                propertySeekersWithoutProperty);
        }

        private DiagnosticCitizenCounts CollectDiagnosticCitizenSamples(
            NativeList<Entity> corrupt,
            NativeList<Entity> movingAway,
            NativeList<Entity> commuters,
            NativeList<Entity> homeless,
            NativeList<Entity> notMovedInWithoutProperty,
            NativeList<Entity> propertySeekersWithoutProperty,
            List<string> homelessMismatchDetails)
        {
            DiagnosticCitizenCounts counts = default;
            using NativeArray<Entity> households =
                m_householdQuery.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < households.Length; i++)
            {
                Entity household = households[i];
                Household householdData =
                    EntityManager.GetComponentData<Household>(household);
                bool hasPropertyRenter =
                    EntityManager.HasComponent<PropertyRenter>(household);
                bool hasMovedIn =
                    (householdData.m_Flags & HouseholdFlags.MovedIn) != 0;
                bool isSeekingProperty =
                    IsPropertySeekerEnabled(household);
                bool isHomeless =
                    EntityManager.HasComponent<HomelessHousehold>(household);
                bool isCommuter =
                    EntityManager.HasComponent<CommuterHousehold>(household);
                bool isTourist =
                    EntityManager.HasComponent<TouristHousehold>(household);
                bool isMovingAway =
                    EntityManager.HasComponent<MovingAway>(household);
                bool isNormalHousingHousehold =
                    !isHomeless &&
                    !isCommuter &&
                    !isTourist &&
                    !isMovingAway;
                bool isCurrentCorruptHousehold =
                    isNormalHousingHousehold && !hasPropertyRenter;

                if (isSeekingProperty)
                {
                    counts.PropertySeekerHouseholds++;

                    if (isHomeless)
                        counts.HomelessPropertySeekerHouseholds++;
                    else if (isNormalHousingHousehold)
                        counts.NormalPropertySeekerHouseholds++;
                }

                if (isNormalHousingHousehold &&
                    !hasPropertyRenter &&
                    !hasMovedIn)
                {
                    counts.NoPropertyRenterNotMovedInHouseholds++;
                    AddSample(
                        notMovedInWithoutProperty,
                        household,
                        kReportSampleLimit);
                }

                if (isNormalHousingHousehold &&
                    !hasPropertyRenter &&
                    isSeekingProperty)
                {
                    counts.NoPropertyRenterPropertySeekerHouseholds++;
                    AddSample(
                        propertySeekersWithoutProperty,
                        household,
                        kReportSampleLimit);
                }

                DynamicBuffer<HouseholdCitizen> members =
                    EntityManager.GetBuffer<HouseholdCitizen>(household);

                for (int j = 0; j < members.Length; j++)
                {
                    Entity citizen = members[j].m_Citizen;
                    if (!IsEligibleCitizen(citizen))
                        continue;

                    if (isCurrentCorruptHousehold)
                    {
                        counts.Corrupt++;
                        AddSample(
                            corrupt,
                            citizen,
                            kCorruptSampleLimit);

                        if (!hasMovedIn)
                            counts.CorruptNotMovedInCitizens++;

                        if (isSeekingProperty)
                            counts.CorruptPropertySeekerCitizens++;
                    }

                    if (isMovingAway && !hasPropertyRenter)
                    {
                        counts.MovingAway++;
                        AddSample(
                            movingAway,
                            citizen,
                            kReportSampleLimit);
                    }

                    if (isCommuter)
                    {
                        counts.Commuters++;
                        AddSample(
                            commuters,
                            citizen,
                            kReportSampleLimit);
                    }

                    if (isHomeless)
                    {
                        counts.HomelessHouseholdMembers++;
                        AddSample(
                            homeless,
                            citizen,
                            kReportSampleLimit);
                        HomelessExclusionReason reason =
                            GetHomelessExclusionReason(citizen);

                        if (reason == HomelessExclusionReason.None)
                        {
                            counts.HomelessValid++;
                        }
                        else
                        {
                            CountHomelessExclusion(ref counts, reason);
                            if (reason ==
                                HomelessExclusionReason.MissingValidCitizen)
                            {
                                CountMissingValidCitizenState(
                                    ref counts,
                                    household);
                            }

                            if (homelessMismatchDetails.Count <
                                kReportSampleLimit)
                            {
                                homelessMismatchDetails.Add(
                                    FormatHomelessMismatch(
                                        citizen,
                                        household,
                                        reason));
                            }
                        }

                    }
                }
            }

            return counts;
        }

        private bool IsPropertySeekerEnabled(Entity household)
        {
            return
                EntityManager.HasComponent<PropertySeeker>(household) &&
                EntityManager.IsComponentEnabled<PropertySeeker>(household);
        }

        private int CountPropertySeekerHouseholds()
        {
            int total = 0;

            using NativeArray<Entity> households =
                m_householdQuery.ToEntityArray(Allocator.TempJob);

            for (int i = 0; i < households.Length; i++)
            {
                Entity household = households[i];
                if (!IsPropertySeekerEnabled(household))
                    continue;

                total++;
            }

            return total;
        }

        private static void CountHomelessExclusion(
            ref DiagnosticCitizenCounts counts,
            HomelessExclusionReason reason)
        {
            switch (reason)
            {
                case HomelessExclusionReason.MissingCitizen:
                    counts.HomelessMissingCitizen++;
                    break;
                case HomelessExclusionReason.Tourist:
                    counts.HomelessTourist++;
                    break;
                case HomelessExclusionReason.Commuter:
                    counts.HomelessCommuter++;
                    break;
                case HomelessExclusionReason.MissingValidCitizen:
                    counts.HomelessMissingValidCitizen++;
                    break;
                case HomelessExclusionReason.Dead:
                    counts.HomelessDead++;
                    break;
                case HomelessExclusionReason.MissingHomelessFlag:
                    counts.HomelessMissingFlag++;
                    break;
            }
        }

        private void CountMissingValidCitizenState(
            ref DiagnosticCitizenCounts counts,
            Entity household)
        {
            if (EntityManager.HasComponent<MovingAway>(household))
            {
                counts.HomelessMissingValidMovingAway++;
                return;
            }

            Household householdData =
                EntityManager.GetComponentData<Household>(household);

            if ((householdData.m_Flags & HouseholdFlags.MovedIn) == 0)
            {
                counts.HomelessMissingValidNotMovedIn++;
            }
            else
            {
                counts.HomelessMissingValidMovedInMismatch++;
            }
        }

        private string FormatHomelessMismatch(
            Entity citizen,
            Entity household,
            HomelessExclusionReason reason)
        {
            Household householdData =
                EntityManager.GetComponentData<Household>(household);
            bool movedIn =
                (householdData.m_Flags & HouseholdFlags.MovedIn) != 0;
            bool movingAway =
                EntityManager.HasComponent<MovingAway>(household);

            string property = "no component";
            if (EntityManager.HasComponent<PropertyRenter>(household))
            {
                property = FormatOptionalEntity(
                    EntityManager.GetComponentData<PropertyRenter>(household)
                        .m_Property);
            }

            string tempHome = "none";
            if (EntityManager.HasComponent<HomelessHousehold>(household))
            {
                tempHome = FormatOptionalEntity(
                    EntityManager.GetComponentData<HomelessHousehold>(household)
                        .m_TempHome);
            }

            return
                $"{FormatIndexVersion(citizen)} | {reason} | " +
                $"household {FormatIndexVersion(household)} | " +
                $"MovedIn {FormatYesNo(movedIn)} | " +
                $"MovingAway {FormatYesNo(movingAway)} | " +
                $"property {property} | temp home {tempHome}";
        }

        private static string FormatOptionalEntity(Entity entity) =>
            entity == Entity.Null ? "none" : FormatIndexVersion(entity);

        private static string FormatYesNo(bool value) => value ? "yes" : "no";

        private static void AddSample(
            NativeList<Entity> samples,
            Entity entity,
            int limit)
        {
            if (samples.Length < limit)
                samples.Add(entity);
        }

        private static void AppendEntitySection(
            StringBuilder report,
            string title,
            int total,
            NativeList<Entity> samples)
        {
            report.AppendLine(
                $"{title}: {total:N0} total | {samples.Length:N0} IDs");
            report.Append(ReportText("IdsLabel", "IDs: "));

            if (samples.Length == 0)
            {
                report.AppendLine(ReportText("None", "(none)"));
                report.AppendLine();
                return;
            }

            for (int i = 0; i < samples.Length; i++)
            {
                if (i > 0)
                    report.Append(", ");

                report.Append(FormatIndexVersion(samples[i]));
            }

            report.AppendLine();
            report.AppendLine();
        }
    }
}
