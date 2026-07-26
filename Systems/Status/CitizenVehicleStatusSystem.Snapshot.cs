// CitizenVehicleStatusSystem.Snapshot.cs
using System;

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

            public int BicycleTotal;
            public int BicycleActive;
            public int BicycleParked;
            public int BicycleTransitioning;
            public int BicycleVisibleParked;
            public int BicycleHiddenAtOutsideConnection;
            public int BicycleHiddenOther;
            public int BicycleOwnershipMismatch;

            public DateTime CapturedAt;
        }
    }
}
