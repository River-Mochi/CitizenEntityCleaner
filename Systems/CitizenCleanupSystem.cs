// CitizenCleanupSystem.cs
using System;
using Colossal.Logging;
using Game;
using Game.Citizens;
using Game.Common;
using Game.Tools;
using Unity.Collections;
using Unity.Entities;

namespace CitizenCleaner
{
    /// <summary>
    /// Cleans citizen entities only when requested from the Options UI.
    /// </summary>
    public partial class CitizenCleanupSystem : GameSystemBase
    {
        private static readonly ILog s_Log = Mod.log;

        private enum CleanupType { None, Corrupt, Homeless, Commuters, MovingAway }

        private struct DeletionCounts
        {
            public int Corrupt, Homeless, Commuters, MovingAway;

            public void BumpCount(CleanupType type)
            {
                switch (type)
                {
                    case CleanupType.Corrupt: Corrupt++; break;
                    case CleanupType.Homeless: Homeless++; break;
                    case CleanupType.Commuters: Commuters++; break;
                    case CleanupType.MovingAway: MovingAway++; break;
                    case CleanupType.None: break;
                }
            }
        }

        private DeletionCounts m_lastCounts;
        private float m_lastProgressNotified = -1f;
        private bool m_shouldRunCleanup;
        private NativeList<Entity> m_entitiesToCleanup;
        private int m_cleanupIndex;
        private bool m_isChunkedCleanupInProgress;
        private EntityQuery m_householdMemberQuery;
        private EntityQuery m_householdQuery;
        private CCSetting? m_settings;

        public event Action<float>? OnCleanupProgress;
        public event Action? OnCleanupCompleted;
        public event Action? OnCleanupNoWork;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_householdMemberQuery = SystemAPI.QueryBuilder()
                .WithAll<HouseholdMember>()
                .WithNone<Deleted>()
                .Build();

            m_householdQuery = SystemAPI.QueryBuilder()
                .WithAll<Household, HouseholdCitizen>()
                .WithNone<Deleted, Temp>()
                .Build();

            // The UI enables this system only while a cleanup is running.
            Enabled = false;
        }

        protected override void OnUpdate()
        {
            if (m_isChunkedCleanupInProgress)
            {
                ProcessCleanupChunk();
                return;
            }

            if (m_shouldRunCleanup)
            {
                m_shouldRunCleanup = false;
                StartChunkedCleanup();
                return;
            }

            Enabled = false;
        }

        protected override void OnGamePreload(
            Colossal.Serialization.Entities.Purpose purpose,
            GameMode mode)
        {
            base.OnGamePreload(purpose, mode);

            if (mode != GameMode.Game ||
                (purpose != Colossal.Serialization.Entities.Purpose.NewGame &&
                 purpose != Colossal.Serialization.Entities.Purpose.LoadGame))
            {
                return;
            }

            // Never carry a deletion list into another city.
            if (m_entitiesToCleanup.IsCreated)
            {
                m_entitiesToCleanup.Dispose();
                m_entitiesToCleanup = default;
            }

            m_shouldRunCleanup = false;
            m_isChunkedCleanupInProgress = false;
            m_cleanupIndex = 0;
            m_lastProgressNotified = -1f;
            m_lastCounts = default;
            Enabled = false;
        }

        protected override void OnGameLoadingComplete(
            Colossal.Serialization.Entities.Purpose purpose,
            GameMode mode)
        {
            base.OnGameLoadingComplete(purpose, mode);

            if (mode == GameMode.Game &&
                (purpose == Colossal.Serialization.Entities.Purpose.NewGame ||
                 purpose == Colossal.Serialization.Entities.Purpose.LoadGame))
            {
                m_settings?.ResetCitySnapshot();
            }
        }

        public void SetSettings(CCSetting settings)
        {
            m_settings = settings;
        }

        public void TriggerCleanup()
        {
            if (m_shouldRunCleanup || m_isChunkedCleanupInProgress)
                return;

            m_shouldRunCleanup = true;
            Enabled = true;

#if DEBUG
            s_Log.Debug("[Cleanup] Triggered from the Options UI.");
#endif
        }

        public (int totalCitizens, int citizensToClean) GetCitizenStatistics()
        {
            try
            {
                int totalCitizens = m_householdMemberQuery.CalculateEntityCount();
                int citizensToClean = GetCitizensToCleanCount();
                return (totalCitizens, citizensToClean);
            }
            catch (Exception ex)
            {
                s_Log.Warn($"Error getting citizen statistics: {ex.Message}");
                return (0, 0);
            }
        }

        public bool HasAnyCitizenData()
        {
            try
            {
                return !m_householdMemberQuery.IsEmptyIgnoreFilter;
            }
            catch (Exception ex)
            {
                s_Log.Warn($"Unable to check citizen data: {ex.Message}");
                return false;
            }
        }

        protected override void OnDestroy()
        {
            if (m_entitiesToCleanup.IsCreated)
                m_entitiesToCleanup.Dispose();

            base.OnDestroy();
        }
    }
}
