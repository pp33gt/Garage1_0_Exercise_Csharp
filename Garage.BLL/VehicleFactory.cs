using Garage.BLL.DomainEntities;
using System;

namespace Garage.BLL
{
    public class VehicleFactory
    {
        private static int LicenseNumber = 1;

        public static int SetLicenseNumber { set { LicenseNumber = value; } }

        public static AirPlane GetAirPlane(Color color, int numberOfAirPlaneWings)
        {
            return new AirPlane(LicenseNumber++, numberOfWheels: 2, color: color, numberOfAirPlaneWings: numberOfAirPlaneWings);
        }

        public static MotorCycle GetMotorCycle(Color color, int motorCycleTopSpeed)
        {
            return new MotorCycle(LicenseNumber++, numberOfWheels: 2, color: color, motorCycleTopSpeed: motorCycleTopSpeed);
        }

        public static Car GetCar(Color color, int numberOfCarSeats)
        {
            return new Car(LicenseNumber++, numberOfWheels: 4, color: color, numberOfCarSeats: numberOfCarSeats);
        }

        public static Bus GetBus(Color color, int busMaxNumberOfStandingPassengers)
        {
            return new Bus(LicenseNumber++, numberOfWheels: 8, color: color, busMaxNumberOfStandingPassengers: busMaxNumberOfStandingPassengers);
        }

        public static Boat GetBoat(Color color, int numberOfBoatSails)
        {
            return new Boat(LicenseNumber++, numberOfWheels: 0, color: color, numberOfBoatSails: numberOfBoatSails);
        }

        internal class RandomVehicle
        {
            private Random RandomGenerator { get; } = new Random();

            internal Color GetRandomColor()
            {
                var rand = RandomGenerator.Next(0, 5);
                var color = (Color)rand;
                return color;
            }

            internal Vehicle GetRandomVehicle()
            {
                var vehicleType = RandomGenerator.Next(5);
                Vehicle vehicle = null;
                var color = GetRandomColor();

                switch (vehicleType)
                {
                    case 1:
                        vehicle = GetAirPlane(color, RandomGenerator.Next(4));
                        break;
                    case 2:
                        vehicle = GetMotorCycle(color, RandomGenerator.Next(250));
                        break;
                    case 3:
                        vehicle = GetCar(color, RandomGenerator.Next(4));
                        break;
                    case 4:
                        vehicle = GetBus(color, RandomGenerator.Next(55));
                        break;
                    default:
                        vehicle = GetBoat(color, RandomGenerator.Next(4));
                        break;
                }
                return vehicle;
            }
        }

        public static Vehicle GetRandomVehicle()
        {
            var rand = new RandomVehicle();
            return rand.GetRandomVehicle();
        }
    }
}
