// CitizenVehicleStatusSystem.cs
using System;
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

namespace CitizenCleaner
{
    /// <summary>
    /// Builds a read-only personal-vehicle snapshot only when the user requests it.
    /// The system is disabled so it adds no per-frame simulation work.
    /// </summary>
    public sealed partial class CitizenVehicleStatusSystem : GameSystemBase
    {
        public readonly struct Snapshot
        {
            public readonly int CarTotal;
            public readonly int CarActive;
            public readonly int CarParked;
            public readonly int CarTransitioning;
            public readonly int CarParkedOnStreet;
            public readonly int CarParkedAtFacility;
            public readonly int CarHiddenAtFacility;
            public readonly int CarHiddenAtOutsideConnection;
            public readonly int CarParkedOther;
            public readonly int CarHiddenOther;
            public readonly int CarOwnershipMismatch;
            public readonly int CarOcHiddenCityHouseholdOwner;
            public readonly int CarOcHiddenOwnerAtOutsideConnection;
            public readonly int CarOcHiddenNonResidentOrMovingOwner;
            public readonly int CarOcHiddenMissingOrNonHouseholdOwner;
            public readonly int CarOcHiddenOwnershipMismatch;
            public readonly int CarOcHiddenLaneAtOutsideConnection;
            public readonly int CarOcHiddenTripSourceAtOutsideConnection;
            public readonly int CarOcHiddenTripSourceWithoutLane;
            public readonly int CarOcHiddenHomeTarget;
            public readonly int CarOcHiddenKeeperAtOutsideConnection;

            public readonly int BicycleTotal;
            public readonly int BicycleActive;
            public readonly int BicycleParked;
            public readonly int BicycleTransitioning;
            public readonly int BicycleVisibleParked;
            public readonly int BicycleHiddenAtOutsideConnection;
            public readonly int BicycleHiddenOther;
            public readonly int BicycleOwnershipMismatch;

            public readonly DateTime CapturedAt;

            public Snapshot(
                int carTotal,
                int carActive,
                int carParked,
                int carTransitioning,
                int carParkedOnStreet,
                int carParkedAtFacility,
                int carHiddenAtFacility,
                int carHiddenAtOutsideConnection,
                int carParkedOther,
                int carHiddenOther,
                int carOwnershipMismatch,
                int carOcHiddenCityHouseholdOwner,
                int carOcHiddenOwnerAtOutsideConnection,
                int carOcHiddenNonResidentOrMovingOwner,
                int carOcHiddenMissingOrNonHouseholdOwner,
                int carOcHiddenOwnershipMismatch,
                int carOcHiddenLaneAtOutsideConnection,
                int carOcHiddenTripSourceAtOutsideConnection,
                int carOcHiddenTripSourceWithoutLane,
                int carOcHiddenHomeTarget,
                int carOcHiddenKeeperAtOutsideConnection,
                int bicycleTotal,
                int bicycleActive,
                int bicycleParked,
                int bicycleTransitioning,
                int bicycleVisibleParked,
                int bicycleHiddenAtOutsideConnection,
                int bicycleHiddenOther,
                int bicycleOwnershipMismatch,
                DateTime capturedAt)
            {
                CarTotal = carTotal;
                CarActive = carActive;
                CarParked = carParked;
                CarTransitioning = carTransitioning;
                CarParkedOnStreet = carParkedOnStreet;
                CarParkedAtFacility = carParkedAtFacility;
                CarHiddenAtFacility = carHiddenAtFacility;
                CarHiddenAtOutsideConnection = carHiddenAtOutsideConnection;
                CarParkedOther = carParkedOther;
                CarHiddenOther = carHiddenOther;
                CarOwnershipMismatch = carOwnershipMismatch;
                CarOcHiddenCityHouseholdOwner = carOcHiddenCityHouseholdOwner;
                CarOcHiddenOwnerAtOutsideConnection = carOcHiddenOwnerAtOutsideConnection;
                CarOcHiddenNonResidentOrMovingOwner = carOcHiddenNonResidentOrMovingOwner;
                CarOcHiddenMissingOrNonHouseholdOwner = carOcHiddenMissingOrNonHouseholdOwner;
                CarOcHiddenOwnershipMismatch = carOcHiddenOwnershipMismatch;
                CarOcHiddenLaneAtOutsideConnection = carOcHiddenLaneAtOutsideConnection;
                CarOcHiddenTripSourceAtOutsideConnection = carOcHiddenTripSourceAtOutsideConnection;
                CarOcHiddenTripSourceWithoutLane = carOcHiddenTripSourceWithoutLane;
                CarOcHiddenHomeTarget = carOcHiddenHomeTarget;
                CarOcHiddenKeeperAtOutsideConnection = carOcHiddenKeeperAtOutsideConnection;

                BicycleTotal = bicycleTotal;
                BicycleActive = bicycleActive;
                BicycleParked = bicycleParked;
                BicycleTransitioning = bicycleTransitioning;
                BicycleVisibleParked = bicycleVisibleParked;
                BicycleHiddenAtOutsideConnection = bicycleHiddenAtOutsideConnection;
                BicycleHiddenOther = bicycleHiddenOther;
                BicycleOwnershipMismatch = bicycleOwnershipMismatch;

                CapturedAt = capturedAt;
            }
        }

        private EntityQuery m_personalVehicleQuery;

        protected override void OnCreate()
        {
            base.OnCreate();

            m_personalVehicleQuery = SystemAPI.QueryBuilder()
                .WithAll<Game.Vehicles.PersonalCar>()
                .WithNone<CarTrailer, Deleted, Temp>()
                .WithNone<Destroyed>()
                .Build();

            // Snapshots are built synchronously by Refresh Counts or the debug button.
            Enabled = false;
        }

        protected override void OnUpdate()
        {
        }

        public Snapshot BuildSnapshot()
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

            bool IsOutsideConnectionLocation(Entity entity)
            {
                Entity current = entity;
                for (var depth = 0; depth < 8 && current != Entity.Null; depth++)
                {
                    if (outsideConnectionLookup.HasComponent(current) ||
                        outsideObjectLookup.HasComponent(current))
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

                DynamicBuffer<HouseholdCitizen> members = householdCitizenLookup[household];
                for (var i = 0; i < members.Length; i++)
                {
                    Entity citizen = members[i].m_Citizen;
                    if (!currentBuildingLookup.HasComponent(citizen))
                        continue;

                    Entity location = currentBuildingLookup[citizen].m_CurrentBuilding;
                    if (IsOutsideConnectionLocation(location))
                        return true;
                }

                return false;
            }

            bool IsParkingFacilityLane(Entity lane)
            {
                Entity current = lane;
                for (var depth = 0; depth < 8 && current != Entity.Null; depth++)
                {
                    if (garageLaneLookup.HasComponent(current) ||
                        parkingFacilityLookup.HasComponent(current) ||
                        carParkingFacilityLookup.HasComponent(current) ||
                        buildingLookup.HasComponent(current))
                        return true;

                    if (!ownerLookup.HasComponent(current))
                        return false;

                    current = ownerLookup[current].m_Owner;
                }

                return false;
            }

            bool TryGetValidCarOwner(Entity vehicle, out Entity owner)
            {
                owner = Entity.Null;
                if (!ownerLookup.HasComponent(vehicle))
                    return false;

                owner = ownerLookup[vehicle].m_Owner;
                if (owner == Entity.Null || !ownedVehicleLookup.HasBuffer(owner))
                    return false;

                DynamicBuffer<OwnedVehicle> ownedVehicles = ownedVehicleLookup[owner];
                for (var i = 0; i < ownedVehicles.Length; i++)
                {
                    if (ownedVehicles[i].m_Vehicle == vehicle)
                        return true;
                }

                return false;
            }

            bool HasValidBicycleKeeper(Entity bicycle, Game.Vehicles.PersonalCar personalCar)
            {
                Entity keeper = personalCar.m_Keeper;
                return keeper != Entity.Null &&
                       bicycleOwnerLookup.TryGetComponent(keeper, out BicycleOwner bicycleOwner) &&
                       bicycleOwner.m_Bicycle == bicycle;
            }

            var carTotal = 0;
            var carActive = 0;
            var carParked = 0;
            var carParkedOnStreet = 0;
            var carParkedAtFacility = 0;
            var carHiddenAtFacility = 0;
            var carHiddenAtOutsideConnection = 0;
            var carParkedOther = 0;
            var carHiddenOther = 0;
            var carOwnershipMismatch = 0;
            var carOcHiddenCityHouseholdOwner = 0;
            var carOcHiddenOwnerAtOutsideConnection = 0;
            var carOcHiddenNonResidentOrMovingOwner = 0;
            var carOcHiddenMissingOrNonHouseholdOwner = 0;
            var carOcHiddenOwnershipMismatch = 0;
            var carOcHiddenLaneAtOutsideConnection = 0;
            var carOcHiddenTripSourceAtOutsideConnection = 0;
            var carOcHiddenTripSourceWithoutLane = 0;
            var carOcHiddenHomeTarget = 0;
            var carOcHiddenKeeperAtOutsideConnection = 0;

            var bicycleTotal = 0;
            var bicycleActive = 0;
            var bicycleParked = 0;
            var bicycleVisibleParked = 0;
            var bicycleHiddenAtOutsideConnection = 0;
            var bicycleHiddenOther = 0;
            var bicycleOwnershipMismatch = 0;

            using (NativeArray<Entity> vehicles = m_personalVehicleQuery.ToEntityArray(Allocator.Temp))
            {
                for (var i = 0; i < vehicles.Length; i++)
                {
                    Entity vehicle = vehicles[i];
                    bool isBicycle = bicycleLookup.HasComponent(vehicle);
                    bool isParked = parkedLookup.HasComponent(vehicle);
                    bool isActive = !isParked && currentLaneLookup.HasComponent(vehicle);
                    bool isHidden = unspawnedLookup.HasComponent(vehicle);

                    if (isBicycle)
                    {
                        bicycleTotal++;

                        if (!HasValidBicycleKeeper(vehicle, personalCarLookup[vehicle]))
                            bicycleOwnershipMismatch++;

                        if (isParked)
                        {
                            bicycleParked++;
                            Entity lane = parkedLookup[vehicle].m_Lane;
                            bool sourceAtOutsideConnection =
                                tripSourceLookup.TryGetComponent(vehicle, out TripSource bicycleTripSource) &&
                                IsOutsideConnectionLocation(bicycleTripSource.m_Source);

                            if (!isHidden)
                                bicycleVisibleParked++;
                            else if (
                                IsOutsideConnectionLocation(lane) ||
                                sourceAtOutsideConnection)
                                bicycleHiddenAtOutsideConnection++;
                            else
                                bicycleHiddenOther++;
                        }
                        else if (isActive)
                        {
                            bicycleActive++;
                        }

                        continue;
                    }

                    carTotal++;

                    bool hasValidCarOwner = TryGetValidCarOwner(vehicle, out Entity carOwner);
                    if (!hasValidCarOwner)
                        carOwnershipMismatch++;

                    if (isParked)
                    {
                        carParked++;
                        Entity lane = parkedLookup[vehicle].m_Lane;
                        bool laneAtOutsideConnection =
                            IsOutsideConnectionLocation(lane);
                        bool tripSourceAtOutsideConnection =
                            tripSourceLookup.TryGetComponent(vehicle, out TripSource tripSource) &&
                            IsOutsideConnectionLocation(tripSource.m_Source);
                        bool isOutsideConnection =
                            isHidden &&
                            (laneAtOutsideConnection || tripSourceAtOutsideConnection);
                        bool isParkingFacility = IsParkingFacilityLane(lane);

                        // These buckets are deliberately exclusive, so they add up to CarParked.
                        if (isOutsideConnection)
                        {
                            carHiddenAtOutsideConnection++;

                            if (laneAtOutsideConnection)
                                carOcHiddenLaneAtOutsideConnection++;

                            if (tripSourceAtOutsideConnection)
                            {
                                carOcHiddenTripSourceAtOutsideConnection++;
                                if (lane == Entity.Null)
                                    carOcHiddenTripSourceWithoutLane++;
                            }

                            Game.Vehicles.PersonalCar personalCar = personalCarLookup[vehicle];
                            if ((personalCar.m_State & PersonalCarFlags.HomeTarget) != 0)
                                carOcHiddenHomeTarget++;

                            Entity keeper = personalCar.m_Keeper;
                            if (keeper != Entity.Null &&
                                currentBuildingLookup.HasComponent(keeper) &&
                                IsOutsideConnectionLocation(currentBuildingLookup[keeper].m_CurrentBuilding))
                            {
                                carOcHiddenKeeperAtOutsideConnection++;
                            }

                            if (!hasValidCarOwner)
                                carOcHiddenOwnershipMismatch++;

                            // Owner-location buckets are separate from backlink validity.
                            // Together these four buckets add up to CarHiddenAtOutsideConnection.
                            if (carOwner == Entity.Null)
                            {
                                carOcHiddenMissingOrNonHouseholdOwner++;
                            }
                            else if (IsOutsideConnectionLocation(carOwner))
                            {
                                carOcHiddenOwnerAtOutsideConnection++;
                            }
                            else if (!householdLookup.HasComponent(carOwner))
                            {
                                carOcHiddenMissingOrNonHouseholdOwner++;
                            }
                            else if (IsHouseholdAtOutsideConnection(carOwner))
                            {
                                carOcHiddenOwnerAtOutsideConnection++;
                            }
                            else if (
                                commuterHouseholdLookup.HasComponent(carOwner) ||
                                touristHouseholdLookup.HasComponent(carOwner) ||
                                movingAwayLookup.HasComponent(carOwner))
                            {
                                carOcHiddenNonResidentOrMovingOwner++;
                            }
                            else
                            {
                                carOcHiddenCityHouseholdOwner++;
                            }
                        }
                        else if (isParkingFacility)
                        {
                            carParkedAtFacility++;
                            if (isHidden)
                                carHiddenAtFacility++;
                        }
                        else if (!isHidden && parkingLaneLookup.HasComponent(lane))
                        {
                            carParkedOnStreet++;
                        }
                        else
                        {
                            carParkedOther++;
                            if (isHidden)
                                carHiddenOther++;
                        }
                    }
                    else if (isActive)
                    {
                        carActive++;
                    }
                }
            }

            return new Snapshot(
                carTotal,
                carActive,
                carParked,
                carTotal - carActive - carParked,
                carParkedOnStreet,
                carParkedAtFacility,
                carHiddenAtFacility,
                carHiddenAtOutsideConnection,
                carParkedOther,
                carHiddenOther,
                carOwnershipMismatch,
                carOcHiddenCityHouseholdOwner,
                carOcHiddenOwnerAtOutsideConnection,
                carOcHiddenNonResidentOrMovingOwner,
                carOcHiddenMissingOrNonHouseholdOwner,
                carOcHiddenOwnershipMismatch,
                carOcHiddenLaneAtOutsideConnection,
                carOcHiddenTripSourceAtOutsideConnection,
                carOcHiddenTripSourceWithoutLane,
                carOcHiddenHomeTarget,
                carOcHiddenKeeperAtOutsideConnection,
                bicycleTotal,
                bicycleActive,
                bicycleParked,
                bicycleTotal - bicycleActive - bicycleParked,
                bicycleVisibleParked,
                bicycleHiddenAtOutsideConnection,
                bicycleHiddenOther,
                bicycleOwnershipMismatch,
                DateTime.Now);
        }
    }
}
