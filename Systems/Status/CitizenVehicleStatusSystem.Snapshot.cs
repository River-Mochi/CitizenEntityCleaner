// File: CitizenVehicleStatusSystem.Snapshot.cs
namespace CitizenCleaner
{
    using System;
    using Unity.Entities;

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
            public int CarParkedLaneNullUnspawned;
            public int CarDummyTraffic;

            public int CarOwnerMismatch;
            public int CarMissingOwner;
            public int CarOwnerMissingBuffer;
            public int CarOwnerMissingBacklink;
            public int CarStreetOwnerMismatch;
            public int CarFacilityOwnerMismatch;
            public int CarOcHiddenOwnerMismatch;
            public int CarOtherParkedOwnerMismatch;

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
            public int BicycleOwnerMismatch;

            public int TrailerTotal;
            public int TrailerHidden;
            public int TrailerMissingController;

            public Entity[]? CarOcHiddenSamples;
            public Entity[]? CarOcHiddenDirectOwnerSamples;
            public Entity[]? CarOcHiddenNonResidentSamples;
            public Entity[]? CarOcHiddenMissingOwnerSamples;
            public Entity[]? CarParkedOtherSamples;
            public Entity[]? CarParkedLaneNullSamples;
            public Entity[]? CarOwnerMismatchSamples;
            public Entity[]? TrailerMissingControllerSamples;

            public DateTime CapturedAt;
        }
    }
}
