// CitizenVehicleStatusSystem.Snapshot.cs
using System;
using Unity.Entities;

namespace CitizenCleaner
{
    public sealed partial class CitizenVehicleStatusSystem
    {
        public struct Snapshot
        {
            public int CarTotal;
            public int CarActive;
            public int CarParked;
            public int CarTransitioning;

            public int CarParkedOnStreet;
            public int CarParkedAtFacility;
            public int CarHiddenAtFacility;
            public int CarHiddenAtOutsideConnection;
            public int CarParkedOther;
            public int CarHiddenOther;
            public int CarOtherLaneNull;
            public int CarOtherHiddenParkingLane;
            public int CarOtherHiddenNonParkingLane;
            public int CarOtherVisibleNonParkingLane;
            public int CarParkedLaneNull;
            public int CarDummyTraffic;

            public int CarOwnershipMismatch;
            public int CarMissingOwner;
            public int CarOwnerMissingBuffer;
            public int CarOwnerMissingBacklink;
            public int CarStreetOwnershipMismatch;
            public int CarFacilityOwnershipMismatch;
            public int CarOcHiddenOwnershipMismatch;
            public int CarOtherParkedOwnershipMismatch;

            public int CarOcHiddenCityHouseholdOwner;
            public int CarOcHiddenHouseholdAtOutsideConnection;
            public int CarOcHiddenDirectOutsideConnectionOwner;
            public int CarOcHiddenNonResidentOrMovingOwner;
            public int CarOcHiddenMissingOrNonHouseholdOwner;
            public int CarOcHiddenLaneAtOutsideConnection;
            public int CarOcHiddenTripSourceAtOutsideConnection;
            public int CarOcHiddenTripSourceWithoutLane;
            public int CarOcHiddenHomeTarget;
            public int CarOcHiddenKeeperAtOutsideConnection;
            public int CarOcHiddenDummyTraffic;
            public int CarOcHiddenDirectOwnerDummyTraffic;

            public int BicycleTotal;
            public int BicycleActive;
            public int BicycleParked;
            public int BicycleTransitioning;
            public int BicycleVisibleParked;
            public int BicycleHiddenAtOutsideConnection;
            public int BicycleHiddenOther;
            public int BicycleOwnershipMismatch;

            public int TrailerTotal;
            public int TrailerHidden;
            public int TrailerMissingController;

            public Entity[]? CarOcHiddenSamples;
            public Entity[]? CarOcHiddenDirectOwnerSamples;
            public Entity[]? CarOcHiddenNonResidentSamples;
            public Entity[]? CarOcHiddenMissingOwnerSamples;
            public Entity[]? CarParkedOtherSamples;
            public Entity[]? CarParkedLaneNullSamples;
            public Entity[]? CarOwnershipMismatchSamples;
            public Entity[]? TrailerMissingControllerSamples;

            public DateTime CapturedAt;
        }
    }
}
