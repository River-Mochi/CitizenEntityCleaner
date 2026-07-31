// File: CitizenVehicleStatusSystem.cs
namespace CitizenCleaner
{
    using System;
    using System.Collections.Generic;
    using Game;
    using Game.Agents;
    using Game.Buildings;
    using Game.Citizens;
    using Game.Common;
    using Game.Net;
    using Game.Objects;
    using Game.Tools;
    using Game.Vehicles;
    using Unity.Collections;
    using Unity.Entities;

    /// <summary>
    /// Builds a personal-vehicle snapshot only when requested from the Options UI.
    /// </summary>
    public sealed partial class CitizenVehicleStatusSystem : GameSystemBase
    {
        private const int kVehicleSampleLimit = 10;

        private enum CarOwnerIssue
        {
            None,
            MissingOwner,
            OwnerMissingBuffer,
            OwnerMissingBacklink,
        }

        private EntityQuery m_PersonalVehicleQuery;
        private EntityQuery m_TrailerQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_PersonalVehicleQuery = SystemAPI.QueryBuilder()
                .WithAll<Game.Vehicles.PersonalCar>()
                .WithNone<CarTrailer, Deleted, Temp>()
                .WithNone<Destroyed, OutOfControl>()
                .Build();

            m_TrailerQuery = SystemAPI.QueryBuilder()
                .WithAll<Game.Vehicles.PersonalCar, CarTrailer>()
                .WithNone<Deleted, Temp>()
                .WithNone<Destroyed, OutOfControl>()
                .Build();

            // BuildSnapshot is called directly; this system never needs an update tick.
            Enabled = false;
        }

        protected override void OnUpdate()
        {
        }

        public Snapshot BuildSnapshot(bool collectSamples = false)
        {
            ComponentLookup<Game.Vehicles.PersonalCar> personalCarLookup =
                GetComponentLookup<Game.Vehicles.PersonalCar>(isReadOnly: true);
            ComponentLookup<Bicycle> bicycleLookup =
                GetComponentLookup<Bicycle>(isReadOnly: true);
            ComponentLookup<ParkedCar> parkedLookup =
                GetComponentLookup<ParkedCar>(isReadOnly: true);
            ComponentLookup<CarCurrentLane> currentLaneLookup =
                GetComponentLookup<CarCurrentLane>(isReadOnly: true);
            ComponentLookup<Unspawned> unspawnedLookup =
                GetComponentLookup<Unspawned>(isReadOnly: true);
            ComponentLookup<TripSource> tripSourceLookup =
                GetComponentLookup<TripSource>(isReadOnly: true);
            ComponentLookup<Owner> ownerLookup =
                GetComponentLookup<Owner>(isReadOnly: true);
            ComponentLookup<Controller> controllerLookup =
                GetComponentLookup<Controller>(isReadOnly: true);
            ComponentLookup<ParkingLane> parkingLaneLookup =
                GetComponentLookup<ParkingLane>(isReadOnly: true);
            ComponentLookup<GarageLane> garageLaneLookup =
                GetComponentLookup<GarageLane>(isReadOnly: true);
            ComponentLookup<Building> buildingLookup =
                GetComponentLookup<Building>(isReadOnly: true);
            ComponentLookup<ParkingFacility> parkingFacilityLookup =
                GetComponentLookup<ParkingFacility>(isReadOnly: true);
            ComponentLookup<CarParkingFacility> carParkingFacilityLookup =
                GetComponentLookup<CarParkingFacility>(isReadOnly: true);
            ComponentLookup<Game.Net.OutsideConnection> outsideConnectionLookup =
                GetComponentLookup<Game.Net.OutsideConnection>(isReadOnly: true);
            ComponentLookup<Game.Objects.OutsideConnection> outsideObjectLookup =
                GetComponentLookup<Game.Objects.OutsideConnection>(isReadOnly: true);
            ComponentLookup<Household> householdLookup =
                GetComponentLookup<Household>(isReadOnly: true);
            ComponentLookup<CommuterHousehold> commuterHouseholdLookup =
                GetComponentLookup<CommuterHousehold>(isReadOnly: true);
            ComponentLookup<TouristHousehold> touristHouseholdLookup =
                GetComponentLookup<TouristHousehold>(isReadOnly: true);
            ComponentLookup<MovingAway> movingAwayLookup =
                GetComponentLookup<MovingAway>(isReadOnly: true);
            ComponentLookup<CurrentBuilding> currentBuildingLookup =
                GetComponentLookup<CurrentBuilding>(isReadOnly: true);
            ComponentLookup<BicycleOwner> bicycleOwnerLookup =
                GetComponentLookup<BicycleOwner>(isReadOnly: true);
            BufferLookup<OwnedVehicle> ownedVehicleLookup =
                GetBufferLookup<OwnedVehicle>(isReadOnly: true);
            BufferLookup<HouseholdCitizen> householdCitizenLookup =
                GetBufferLookup<HouseholdCitizen>(isReadOnly: true);

            List<Entity>? ocHiddenSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? ocDirectOwnerSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? ocNonResidentSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? ocMissingOwnerSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? parkedOtherSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? parkedLaneNullSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? ownerMismatchSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;
            List<Entity>? trailerMissingControllerSamples =
                collectSamples ? new List<Entity>(kVehicleSampleLimit) : null;

            static void AddSample(List<Entity>? samples, Entity entity)
            {
                if (samples != null && samples.Count < kVehicleSampleLimit)
                    samples.Add(entity);
            }

            bool IsOutsideConnectionEntity(Entity entity)
            {
                return entity != Entity.Null &&
                    (outsideConnectionLookup.HasComponent(entity) ||
                     outsideObjectLookup.HasComponent(entity));
            }

            // Location helpers follow short Owner chains; depth cap prevents corrupt cycles.
            bool IsOutsideConnectionLocation(Entity entity)
            {
                Entity current = entity;

                for (int depth = 0; depth < 8 && current != Entity.Null; depth++)
                {
                    if (IsOutsideConnectionEntity(current))
                        return true;

                    if (!ownerLookup.HasComponent(current))
                        return false;

                    current = ownerLookup[current].m_Owner;
                }

                return false;
            }

            bool IsHouseholdAtOutsideConnection(Entity household)
            {
                if (!householdCitizenLookup.HasBuffer(household))
                    return false;

                DynamicBuffer<HouseholdCitizen> members =
                    householdCitizenLookup[household];

                for (int i = 0; i < members.Length; i++)
                {
                    Entity citizen = members[i].m_Citizen;
                    if (!currentBuildingLookup.HasComponent(citizen))
                        continue;

                    Entity location =
                        currentBuildingLookup[citizen].m_CurrentBuilding;
                    if (IsOutsideConnectionLocation(location))
                        return true;
                }

                return false;
            }

            bool IsParkingFacilityLane(Entity lane)
            {
                Entity current = lane;

                for (int depth = 0; depth < 8 && current != Entity.Null; depth++)
                {
                    if (garageLaneLookup.HasComponent(current) ||
                        parkingFacilityLookup.HasComponent(current) ||
                        carParkingFacilityLookup.HasComponent(current) ||
                        buildingLookup.HasComponent(current))
                    {
                        return true;
                    }

                    if (!ownerLookup.HasComponent(current))
                        return false;

                    current = ownerLookup[current].m_Owner;
                }

                return false;
            }

            // Match PersonalCarOwnerSystem's owner checks for cars and bicycles.
            CarOwnerIssue GetCarOwnerIssue(
                Entity vehicle,
                out Entity owner)
            {
                owner = Entity.Null;

                if (!ownerLookup.HasComponent(vehicle))
                    return CarOwnerIssue.MissingOwner;

                owner = ownerLookup[vehicle].m_Owner;
                if (owner == Entity.Null)
                    return CarOwnerIssue.MissingOwner;

                if (!ownedVehicleLookup.HasBuffer(owner))
                    return CarOwnerIssue.OwnerMissingBuffer;

                DynamicBuffer<OwnedVehicle> ownedVehicles =
                    ownedVehicleLookup[owner];

                for (int i = 0; i < ownedVehicles.Length; i++)
                {
                    if (ownedVehicles[i].m_Vehicle == vehicle)
                        return CarOwnerIssue.None;
                }

                return CarOwnerIssue.OwnerMissingBacklink;
            }

            bool HasValidBicycleKeeper(
                Entity bicycle,
                Game.Vehicles.PersonalCar personalCar)
            {
                Entity keeper = personalCar.m_Keeper;
                return
                    keeper != Entity.Null &&
                    bicycleOwnerLookup.TryGetComponent(
                        keeper,
                        out BicycleOwner bicycleOwner) &&
                    bicycleOwner.m_Bicycle == bicycle;
            }

            Snapshot snapshot = default;

            using NativeArray<Entity> vehicles =
                m_PersonalVehicleQuery.ToEntityArray(Allocator.Temp);

            for (int i = 0; i < vehicles.Length; i++)
            {
                Entity vehicle = vehicles[i];
                bool isBicycle = bicycleLookup.HasComponent(vehicle);
                bool isParked = parkedLookup.HasComponent(vehicle);
                bool isActive =
                    !isParked && currentLaneLookup.HasComponent(vehicle);
                bool isHidden = unspawnedLookup.HasComponent(vehicle);

                if (isBicycle)
                {
                    snapshot.BicycleTotal++;

                    if (!HasValidBicycleKeeper(
                        vehicle,
                        personalCarLookup[vehicle]))
                    {
                        snapshot.BicycleOwnerMismatch++;
                    }

                    if (isParked)
                    {
                        snapshot.BicycleParked++;
                        Entity lane = parkedLookup[vehicle].m_Lane;
                        bool sourceAtOutsideConnection =
                            tripSourceLookup.TryGetComponent(
                                vehicle,
                                out TripSource bicycleTripSource) &&
                            IsOutsideConnectionLocation(
                                bicycleTripSource.m_Source);

                        if (!isHidden)
                        {
                            snapshot.BicycleVisibleParked++;
                        }
                        else if (
                            IsOutsideConnectionLocation(lane) ||
                            sourceAtOutsideConnection)
                        {
                            snapshot.BicycleHiddenAtOutsideConnection++;
                        }
                        else
                        {
                            snapshot.BicycleHiddenOther++;
                        }
                    }
                    else if (isActive)
                    {
                        snapshot.BicycleActive++;
                    }

                    continue;
                }

                snapshot.CarTotal++;

                Game.Vehicles.PersonalCar personalCar =
                    personalCarLookup[vehicle];
                bool isDummyTraffic =
                    (personalCar.m_State & PersonalCarFlags.DummyTraffic) != 0;
                if (isDummyTraffic)
                    snapshot.CarDummyTraffic++;

                CarOwnerIssue ownerIssue =
                    GetCarOwnerIssue(vehicle, out Entity carOwner);
                bool hasOwnerMismatch =
                    ownerIssue != CarOwnerIssue.None;

                if (hasOwnerMismatch)
                {
                    snapshot.CarOwnerMismatch++;
                    AddSample(ownerMismatchSamples, vehicle);

                    switch (ownerIssue)
                    {
                        case CarOwnerIssue.MissingOwner:
                            snapshot.CarMissingOwner++;
                            break;
                        case CarOwnerIssue.OwnerMissingBuffer:
                            snapshot.CarOwnerMissingBuffer++;
                            break;
                        case CarOwnerIssue.OwnerMissingBacklink:
                            snapshot.CarOwnerMissingBacklink++;
                            break;
                    }
                }

                if (isParked)
                {
                    snapshot.CarParked++;
                    Entity lane = parkedLookup[vehicle].m_Lane;

                    // Null lane alone is not abandonment; track its Unspawned overlap.
                    if (lane == Entity.Null)
                    {
                        snapshot.CarParkedLaneNull++;

                        if (isHidden)
                            snapshot.CarParkedLaneNullUnspawned++;

                        AddSample(parkedLaneNullSamples, vehicle);
                    }

                    bool laneAtOutsideConnection =
                        IsOutsideConnectionLocation(lane);
                    bool tripSourceAtOutsideConnection =
                        tripSourceLookup.TryGetComponent(
                            vehicle,
                            out TripSource tripSource) &&
                        IsOutsideConnectionLocation(tripSource.m_Source);
                    bool isOutsideConnection =
                        isHidden &&
                        (laneAtOutsideConnection ||
                         tripSourceAtOutsideConnection);
                    bool isParkingFacility = IsParkingFacilityLane(lane);

                    // These buckets are exclusive and add up to CarParked.
                    if (isOutsideConnection)
                    {
                        snapshot.CarHiddenAtOutsideConnection++;
                        AddSample(ocHiddenSamples, vehicle);

                        if (isDummyTraffic)
                            snapshot.CarOcHiddenDummyTraffic++;

                        if (hasOwnerMismatch)
                            snapshot.CarOcHiddenOwnerMismatch++;

                        if (laneAtOutsideConnection)
                            snapshot.CarOcHiddenLaneAtOutsideConnection++;

                        if (tripSourceAtOutsideConnection)
                        {
                            snapshot.CarOcHiddenTripSourceAtOutsideConnection++;
                            if (lane == Entity.Null)
                                snapshot.CarOcHiddenTripSourceWithoutLane++;
                        }

                        if ((personalCar.m_State &
                             PersonalCarFlags.HomeTarget) != 0)
                        {
                            snapshot.CarOcHiddenHomeTarget++;
                        }

                        Entity keeper = personalCar.m_Keeper;
                        if (keeper != Entity.Null &&
                            currentBuildingLookup.HasComponent(keeper) &&
                            IsOutsideConnectionLocation(
                                currentBuildingLookup[keeper].m_CurrentBuilding))
                        {
                            snapshot.CarOcHiddenKeeperAtOutsideConnection++;
                        }

                        // These owner buckets add up to all OC-hidden cars.
                        if (carOwner == Entity.Null)
                        {
                            snapshot.CarOcHiddenMissingOrNonHouseholdOwner++;
                            AddSample(ocMissingOwnerSamples, vehicle);
                        }
                        else if (IsOutsideConnectionEntity(carOwner))
                        {
                            snapshot.CarOcHiddenDirectOutsideConnectionOwner++;
                            AddSample(ocDirectOwnerSamples, vehicle);

                            if (isDummyTraffic)
                            {
                                snapshot
                                    .CarOcHiddenDirectOwnerDummyTraffic++;
                            }
                        }
                        else if (!householdLookup.HasComponent(carOwner))
                        {
                            snapshot.CarOcHiddenMissingOrNonHouseholdOwner++;
                            AddSample(ocMissingOwnerSamples, vehicle);
                        }
                        else if (
                            commuterHouseholdLookup.HasComponent(carOwner) ||
                            touristHouseholdLookup.HasComponent(carOwner) ||
                            movingAwayLookup.HasComponent(carOwner))
                        {
                            snapshot.CarOcHiddenNonResidentOrMovingOwner++;
                            AddSample(ocNonResidentSamples, vehicle);
                        }
                        else if (IsHouseholdAtOutsideConnection(carOwner))
                        {
                            snapshot.CarOcHiddenHouseholdAtOutsideConnection++;
                        }
                        else
                        {
                            snapshot.CarOcHiddenCityHouseholdOwner++;
                        }
                    }
                    else if (isParkingFacility)
                    {
                        snapshot.CarParkedAtFacility++;
                        if (isHidden)
                            snapshot.CarHiddenAtFacility++;
                        if (hasOwnerMismatch)
                            snapshot.CarFacilityOwnerMismatch++;
                    }
                    else if (
                        !isHidden &&
                        parkingLaneLookup.HasComponent(lane))
                    {
                        snapshot.CarParkedOnStreet++;
                        if (hasOwnerMismatch)
                            snapshot.CarStreetOwnerMismatch++;
                    }
                    else
                    {
                        snapshot.CarParkedOther++;
                        AddSample(parkedOtherSamples, vehicle);

                        if (isHidden)
                            snapshot.CarHiddenOther++;

                        if (lane == Entity.Null)
                        {
                            snapshot.CarOtherLaneNull++;
                        }
                        else if (parkingLaneLookup.HasComponent(lane))
                        {
                            snapshot.CarOtherHiddenParkingLane++;
                        }
                        else if (isHidden)
                        {
                            snapshot.CarOtherHiddenNonParkingLane++;
                        }
                        else
                        {
                            snapshot.CarOtherVisibleNonParkingLane++;
                        }

                        if (hasOwnerMismatch)
                            snapshot.CarOtherParkedOwnerMismatch++;
                    }
                }
                else if (isActive)
                {
                    snapshot.CarActive++;
                }
            }

            snapshot.CarTransitioning =
                snapshot.CarTotal - snapshot.CarActive - snapshot.CarParked;
            snapshot.BicycleTransitioning =
                snapshot.BicycleTotal -
                snapshot.BicycleActive -
                snapshot.BicycleParked;

            using NativeArray<Entity> trailers =
                m_TrailerQuery.ToEntityArray(Allocator.Temp);

            snapshot.TrailerTotal = trailers.Length;

            for (int i = 0; i < trailers.Length; i++)
            {
                Entity trailer = trailers[i];

                if (unspawnedLookup.HasComponent(trailer))
                    snapshot.TrailerHidden++;

                bool missingController =
                    !controllerLookup.HasComponent(trailer) ||
                    controllerLookup[trailer].m_Controller == Entity.Null;

                if (missingController)
                {
                    snapshot.TrailerMissingController++;
                    AddSample(trailerMissingControllerSamples, trailer);
                }
            }

            snapshot.CarOcHiddenSamples = ocHiddenSamples?.ToArray();
            snapshot.CarOcHiddenDirectOwnerSamples =
                ocDirectOwnerSamples?.ToArray();
            snapshot.CarOcHiddenNonResidentSamples =
                ocNonResidentSamples?.ToArray();
            snapshot.CarOcHiddenMissingOwnerSamples =
                ocMissingOwnerSamples?.ToArray();
            snapshot.CarParkedOtherSamples = parkedOtherSamples?.ToArray();
            snapshot.CarParkedLaneNullSamples =
                parkedLaneNullSamples?.ToArray();
            snapshot.CarOwnerMismatchSamples =
                ownerMismatchSamples?.ToArray();
            snapshot.TrailerMissingControllerSamples =
                trailerMissingControllerSamples?.ToArray();
            snapshot.CapturedAt = DateTime.Now;

            return snapshot;
        }
    }
}
