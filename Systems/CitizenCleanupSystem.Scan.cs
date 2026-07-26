// CitizenCleanupSystem.Scan.cs
using Game.Agents;
using Game.Buildings;
using Game.Citizens;
using Game.Common;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

namespace CitizenCleaner
{
    // PART: Scan (read-only) — selectors, counts, debug preview, helpers
    public partial class CitizenCleanupSystem : SystemBase
    {
        #region Selection Logic
        /// <summary>
        /// Builds deletion set based on toggles (or overrides for debug preview):
        /// - Corrupt households (no PropertyRenter & not homeless/commuter/tourist/moving-away)
        /// - HomelessHousehold members when IncludeHomeless == true
        /// - CommuterHousehold members when IncludeCommuters == true
        /// - Moving-Away (no PropertyRenter) when IncludeMovingAwayNoPR == true
        ///
        /// Notes:
        /// • Optional overrides let callers force a specific combo (e.g., Debug preview runs "Corrupt-only" regardless of checkboxes).
        /// • If "tally" = true, method increments per-category counters in "m_lastCounts" as it builds the candidate list.
        ///     Set tally = false only when need the list/count (e.g., UI refresh or preview).
        /// </summary>
        private NativeList<Entity> GetDeletionCandidates(
            Allocator allocator,
            bool tally = true,
            bool? overrideWantCorrupt = null,
            bool? overrideWantHomeless = null,
            bool? overrideWantCommuters = null,
            bool? overrideWantMovingAwayNoPR = null)
        {
            using NativeArray<HouseholdMember> householdMembers = m_householdMemberQuery.ToComponentDataArray<HouseholdMember>(Allocator.TempJob);
            var candidates = new NativeList<Entity>(math.max(1, householdMembers.Length), allocator);


            // Mirror UI defaults: Corrupt defaults ON; others default OFF
            // Order: override → UI settings → fallback
            var wantCorrupt = ResolveToggle(overrideWantCorrupt, m_settings?.IncludeCorrupt, fallback: true);
            var wantHomeless = ResolveToggle(overrideWantHomeless, m_settings?.IncludeHomeless, fallback: false);
            var wantCommuters = ResolveToggle(overrideWantCommuters, m_settings?.IncludeCommuters, fallback: false);
            var wantMovingAwayNoPR = ResolveToggle(overrideWantMovingAwayNoPR, m_settings?.IncludeMovingAwayNoPR, fallback: false);


            // Early-out: nothing selected
            if (!wantCorrupt && !wantHomeless && !wantCommuters && !wantMovingAwayNoPR)
                return candidates;

            try
            {

                using var processedHouseholds =
                    new NativeHashSet<Entity>(math.max(1, householdMembers.Length), Allocator.TempJob);

                foreach (HouseholdMember householdMember in householdMembers)
                {
                    Entity householdEntity = householdMember.m_Household;

                    // Skip if missing or already processed
                    if (!EntityManager.Exists(householdEntity)) continue;
                    if (!processedHouseholds.Add(householdEntity)) continue;

                    // Must have members buffer, skip households without members list
                    if (!EntityManager.HasBuffer<HouseholdCitizen>(householdEntity)) continue;

                    // Household category flags
                    var hasPropertyRenter = EntityManager.HasComponent<PropertyRenter>(householdEntity);
                    var isHomelessHH = EntityManager.HasComponent<HomelessHousehold>(householdEntity);
                    var isCommuterHH = EntityManager.HasComponent<CommuterHousehold>(householdEntity);
                    var isTouristHH = EntityManager.HasComponent<TouristHousehold>(householdEntity);
                    var isMovingAwayHH = EntityManager.HasComponent<MovingAway>(householdEntity);

                    // MovingAway belongs to the household in the game. Treating it as a citizen
                    // component can split and delete a legitimate household before every member
                    // has started an individual MovingAway trip.
                    var isResidentCorrupt =
                        !hasPropertyRenter &&
                        !isHomelessHH &&
                        !isCommuterHH &&
                        !isTouristHH &&
                        !isMovingAwayHH;

                    var householdMatchesAny =
                        (wantHomeless && isHomelessHH) ||
                        (wantCommuters && isCommuterHH) ||
                        (wantCorrupt && isResidentCorrupt) ||
                        (wantMovingAwayNoPR && isMovingAwayHH && !hasPropertyRenter);

                    if (!householdMatchesAny)
                        continue;

                    // Iterate members and apply per-citizen rules (via shared classifier)
                    DynamicBuffer<HouseholdCitizen> householdCitizens = EntityManager.GetBuffer<HouseholdCitizen>(householdEntity);
                    for (var j = 0; j < householdCitizens.Length; j++)
                    {
                        Entity citizenEntity = householdCitizens[j].m_Citizen;
                        if (!EntityManager.Exists(citizenEntity)) continue;
                        if (EntityManager.HasComponent<Deleted>(citizenEntity)) continue;

                        CleanupType reason = ClassifyCitizenForDeletion(
                            wantCorrupt, wantHomeless, wantCommuters, wantMovingAwayNoPR,
                            isHomelessHH, isCommuterHH, isTouristHH, isMovingAwayHH,
                            hasPropertyRenter);

                        if (reason != CleanupType.None)
                        {
                            // mark for deletion; only update tallies when a real cleanup is run
                            candidates.Add(citizenEntity);
                            if (tally) m_lastCounts.BumpCount(reason);
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                s_Log.Warn($"Error building deletion candidates list: {ex.Message}");
            }

            return candidates;
        }


        /// <summary>
        /// Count citizens to clean based on toggles:
        /// IncludeCorrupt, IncludeHomeless, IncludeCommuters.
        /// </summary>
        private int GetCitizensToCleanCount()
        {
            using NativeList<Entity> candidates = GetDeletionCandidates(Allocator.TempJob, tally: false);
            return candidates.Length;
        }
        #endregion

        #region Helpers
        // Resolve boolean: precedence: override → UI setting → fallback default.
        // Example: ResolveToggle(forced:true,  setting:false, fallback:false) => true
        //          ResolveToggle(forced:null,  setting:true,  fallback:false) => true
        //          ResolveToggle(forced:null,  setting:null,  fallback:true)  => true
        private static bool ResolveToggle(bool? overrideValue, bool? settingValue, bool fallback)
        {
            return overrideValue ?? (settingValue ?? fallback);
        }

        // Formats an Entity as "Index:Version" for Scene Explorer cross-checks and logs.
        private static string FormatIndexVersion(Entity e) => $"{e.Index}:{e.Version}";


        private CleanupType ClassifyCitizenForDeletion(
            bool wantCorrupt,
            bool wantHomeless,
            bool wantCommuters,
            bool wantMovingAwayNoPR,
            bool isHomelessHH,
            bool isCommuterHH,
            bool isTouristHH,
            bool isMovingAwayHH,
            bool hasPropertyRenter)
        {
            // Precedence: Homeless → Commuters → Moving-Away → Corrupt.
            if (wantHomeless && isHomelessHH) return CleanupType.Homeless;
            if (wantCommuters && isCommuterHH) return CleanupType.Commuters;
            if (wantMovingAwayNoPR && isMovingAwayHH && !hasPropertyRenter) return CleanupType.MovingAway;

            if (wantCorrupt &&
                !hasPropertyRenter &&
                !isHomelessHH &&
                !isCommuterHH &&
                !isTouristHH &&
                !isMovingAwayHH)
            {
                return CleanupType.Corrupt;
            }

            return CleanupType.None;
        }

        #endregion
    }
}
