// File: CitizenCleanupSystem.Scan.cs
namespace CitizenCleaner
{
    using System;
    using Game.Agents;
    using Game.Buildings;
    using Game.Citizens;
    using Game.Common;
    using Game.Tools;
    using Unity.Collections;
    using Unity.Entities;
    using Unity.Mathematics;

    public partial class CitizenCleanupSystem
    {
        private NativeList<Entity> GetDeletionCandidates(
            Allocator allocator,
            bool tally = true)
        {
            int capacity = math.max(1, m_householdMemberQuery.CalculateEntityCount());
            NativeList<Entity> candidates = new NativeList<Entity>(capacity, allocator);

            bool wantCorrupt = m_settings?.IncludeCorrupt ?? true;
            bool wantHomeless = m_settings?.IncludeHomeless ?? false;
            bool wantCommuters = m_settings?.IncludeCommuters ?? false;
            bool wantMovingAway = m_settings?.IncludeMovingAwayNoPR ?? false;

            if (!wantCorrupt && !wantHomeless && !wantCommuters && !wantMovingAway)
                return candidates;

            try
            {
                using NativeArray<Entity> households =
                    m_householdQuery.ToEntityArray(Allocator.TempJob);

                for (int i = 0; i < households.Length; i++)
                {
                    Entity household = households[i];
                    CleanupType type = ClassifyHousehold(household);

                    bool selected = type switch
                    {
                        CleanupType.Corrupt => wantCorrupt,
                        CleanupType.Homeless => wantHomeless,
                        CleanupType.Commuters => wantCommuters,
                        CleanupType.MovingAway => wantMovingAway,
                        _ => false,
                    };

                    if (!selected)
                        continue;

                    DynamicBuffer<HouseholdCitizen> members =
                        EntityManager.GetBuffer<HouseholdCitizen>(household);

                    for (int j = 0; j < members.Length; j++)
                    {
                        Entity citizen = members[j].m_Citizen;
                        if (!IsEligibleCitizen(citizen, type))
                            continue;

                        candidates.Add(citizen);
                        if (tally)
                            m_lastCounts.BumpCount(type);
                    }
                }
            }
            catch (Exception ex)
            {
                // Never continue with a partial deletion list.
                candidates.Clear();
                if (tally)
                    m_lastCounts = default;

                s_Log.Error($"Citizen scan failed; cleanup cancelled: {ex.Message}");
#if DEBUG
                s_Log.Debug(ex.ToString());
#endif
            }

            return candidates;
        }

        private int GetCitizensToCleanCount()
        {
            using NativeList<Entity> candidates =
                GetDeletionCandidates(Allocator.TempJob, tally: false);
            return candidates.Length;
        }

        private CleanupType ClassifyHousehold(Entity household)
        {
            bool hasPropertyRenter =
                EntityManager.HasComponent<PropertyRenter>(household);
            bool isHomeless =
                EntityManager.HasComponent<HomelessHousehold>(household);
            bool isCommuter =
                EntityManager.HasComponent<CommuterHousehold>(household);
            bool isTourist =
                EntityManager.HasComponent<TouristHousehold>(household);
            bool isMovingAway =
                EntityManager.HasComponent<MovingAway>(household);

            if (isHomeless)
                return CleanupType.Homeless;

            if (isCommuter)
                return CleanupType.Commuters;

            // MovingAway belongs to the household, before every member gets its trip.
            if (isMovingAway && !hasPropertyRenter)
                return CleanupType.MovingAway;

            if (!hasPropertyRenter && !isTourist && !isMovingAway)
                return CleanupType.Corrupt;

            return CleanupType.None;
        }

        private enum HomelessExclusionReason
        {
            None,
            MissingCitizen,
            Tourist,
            Commuter,
            MissingValidCitizen,
            Dead,
            MissingHomelessFlag,
        }

        private bool IsEligibleCitizen(Entity citizen)
        {
            return
                EntityManager.Exists(citizen) &&
                !EntityManager.HasComponent<Deleted>(citizen) &&
                !EntityManager.HasComponent<Temp>(citizen);
        }

        private bool IsEligibleCitizen(Entity citizen, CleanupType type)
        {
            if (!IsEligibleCitizen(citizen))
                return false;

            return
                type != CleanupType.Homeless ||
                GetHomelessExclusionReason(citizen) ==
                    HomelessExclusionReason.None;
        }

        private HomelessExclusionReason GetHomelessExclusionReason(
            Entity citizen)
        {
            if (!EntityManager.HasComponent<Citizen>(citizen))
                return HomelessExclusionReason.MissingCitizen;

            Citizen citizenData =
                EntityManager.GetComponentData<Citizen>(citizen);

            if ((citizenData.m_State & CitizenFlags.Tourist) != 0)
                return HomelessExclusionReason.Tourist;

            if ((citizenData.m_State & CitizenFlags.Commuter) != 0)
                return HomelessExclusionReason.Commuter;

            if ((citizenData.m_State & CitizenFlags.ValidCitizen) == 0)
                return HomelessExclusionReason.MissingValidCitizen;

            if (EntityManager.HasComponent<HealthProblem>(citizen) &&
                CitizenUtils.IsDead(
                    EntityManager.GetComponentData<HealthProblem>(citizen)))
            {
                return HomelessExclusionReason.Dead;
            }

            if ((citizenData.m_State & CitizenFlags.Homeless) == 0)
                return HomelessExclusionReason.MissingHomelessFlag;

            return HomelessExclusionReason.None;
        }

        private static string FormatIndexVersion(Entity entity) =>
            $"{entity.Index}:{entity.Version}";
    }
}
