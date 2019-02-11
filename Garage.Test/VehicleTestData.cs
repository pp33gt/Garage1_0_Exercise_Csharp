using Garage.BLL;
using Garage.BLL.DomainEntities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Garage.Test
{
    class VehicleTestData
    {
        private VehicleFactory VehicleFactory { get; } = new VehicleFactory();

        internal static Car Car1 { get; } = VehicleFactory.GetCar(Color.Yellow, 4);
        internal static AirPlane AirPlane1 { get; } = VehicleFactory.GetAirPlane(Color.Yellow, 2);
        internal static MotorCycle MotorCycle1 { get; } = VehicleFactory.GetMotorCycle(Color.Red, 250);
        internal static Bus Bus1 { get; } = VehicleFactory.GetBus(Color.Red, 55);
        internal static Boat Boat1 { get; } = VehicleFactory.GetBoat(Color.Blue, 4);

        internal static Car Car2 { get; } = VehicleFactory.GetCar(Color.Yellow, 2);
        internal static AirPlane AirPlane2 { get; } = VehicleFactory.GetAirPlane(Color.Blue, 2);
        internal static MotorCycle MotorCycle2 { get; } = VehicleFactory.GetMotorCycle(Color.Red, 100);
        internal static Bus Bus2 { get; } = VehicleFactory.GetBus(Color.Black, 6);
        internal static Boat Boat2 { get; } = VehicleFactory.GetBoat(Color.Blue, 2);

        internal static Car Car3 { get; } = VehicleFactory.GetCar(Color.Green, 4);
        internal static AirPlane AirPlane3 { get; } = VehicleFactory.GetAirPlane(Color.Green, 4);
        internal static MotorCycle MotorCycle3 { get; } = VehicleFactory.GetMotorCycle(Color.Green, 100);

        internal static List<Vehicle> Vehicles = new List<Vehicle>() {
            Car1,
            AirPlane1,
            MotorCycle1,
            Bus1,
            Boat1,

            Car2,
            AirPlane2,
            MotorCycle2,
            Bus2,
            Boat2,

            Car3,
            AirPlane3,
            MotorCycle3
        };

    }
}
