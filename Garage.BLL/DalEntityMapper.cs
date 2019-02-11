using System;
using System.Collections.Generic;
using Garage.BLL.DomainEntities;
using Garage.DAL.Entities;
using Garage.DAL;
using System.Linq;

namespace Garage.BLL
{
    public partial class GarageHandler
    {
        private static class DalEntityMapper
        {

            public static List<Vehicle> DalObjectsToVehicles(List<VehicleEntity> vehiclesSave)
            {
                List<Vehicle> vehicles = new List<Vehicle>();
                vehicles = vehiclesSave.Select(a => {
                    var licenseNo = a.LicenseNo;
                    var numberOfWheels = a.NumberOfWheels;
                    var color = ColorHelper.ParseColor(a.Color.ToString());

                    Vehicle v = null;

                    if (a is AirPlaneEntity aPlane)
                    {
                        v = new AirPlane(licenseNo, numberOfWheels, color, aPlane.NumberOfAirPlaneWings);
                    }
                    else if (a is BoatEntity aBoat)
                    {
                        v = new Boat(licenseNo, numberOfWheels, color, aBoat.NumberOfBoatSails);
                    }
                    else if (a is BusEntity aBuss)
                    {
                        v = new Bus(licenseNo, numberOfWheels, color, aBuss.BusMaxNumberOfStandingPassengers);
                    }
                    else if (a is CarEntity aCar)
                    {
                        v = new Car(licenseNo, numberOfWheels, color, aCar.NumberOfCarSeats);
                    }
                    else if (a is MotorCycleEntity aMc)
                    {
                        v = new MotorCycle(licenseNo, numberOfWheels, color, aMc.MotorCycleTopSpeed);
                    }
                    if(v == null)
                    {
                        throw new Exception("Unhandled Vehicle Type");
                    }
                    return v;
                }).ToList();
                return vehicles;
            }

            public static List<VehicleEntity> VehicleToDalObjects(List<Vehicle> vehicles)
            {
                var vehiclesToSave = new List<VehicleEntity>();

                vehiclesToSave = vehicles.Select(a => {
                    VehicleEntity v = null;
                    if (a is AirPlane aPlane)
                    {
                        v = new AirPlaneEntity
                        {
                            NumberOfAirPlaneWings = aPlane.NumberOfAirPlaneWings
                        };
                    }
                    else if (a is Boat aBoat)
                    {
                        v = new BoatEntity
                        {
                            NumberOfBoatSails = aBoat.NumberOfBoatSails
                        };
                    }
                    else if (a is Bus aBuss)
                    {
                        v = new BusEntity
                        {
                            BusMaxNumberOfStandingPassengers = aBuss.BusMaxNumberOfStandingPassengers
                        };

                    }
                    else if (a is Car aCar)
                    {
                        v = new CarEntity
                        {
                            NumberOfCarSeats = aCar.NumberOfCarSeats
                        };
                    }
                    else if (a is MotorCycle aMc)
                    {
                        v = new MotorCycleEntity
                        {
                            MotorCycleTopSpeed = aMc.MotorCycleTopSpeed
                        };
                    }

                    if (v == null)
                    {
                        throw new Exception("Unhandled Vehicle Type");
                    }

                    v.LicenseNo = a.LicenseNo;
                    v.Color = ColorHelperDAL.ParseColor(a.Color.ToString());
                    v.NumberOfWheels = a.NumberOfWheels;
                    return v;
                }).ToList();
               
                return vehiclesToSave;
            }
        }
    }
}
