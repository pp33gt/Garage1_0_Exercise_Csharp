using Garage.BLL;
using Garage.BLL.DomainEntities;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Garage.BLL.ConsoleUI;

namespace Garage.Test
{
    [TestClass]
    public partial class GarageHandlerTests
    {
        private ConsoleMock Console { get; set; } = null;
        private GarageHandler GarageHandler { get; set; } = null;

        private bool TryParkVehicle<T>(T vehicle) where T : Vehicle
        {
            return GarageHandler.TryParkVehicle(vehicle);
        }

        private IEnumerable<ParkResult> ParkVehicles(IEnumerable<Vehicle> vehicles)
        {
            var results = new List<ParkResult>();
            foreach(var vehicle in vehicles)
            {
                var resPark = TryParkVehicle(vehicle);
                results.Add(new ParkResult() { Vehicle = vehicle, Result = resPark });
            }
            return results;
        }
       

        [TestMethod]
        public void ListVehicles_13Vehicles_OutputIsMatching()
        {
            // Arrange
            Console = new ConsoleMock();

            GarageHandler = new GarageHandler();
            UIConsoleHandler consoleHandlerUi = new UIConsoleHandler(Console, GarageHandler);

            GarageHandler.CreateGarage(numberOfVehicles: 13);

            ParkVehicles(VehicleTestData.Vehicles);

            // Act
            consoleHandlerUi.UserListVehicles();

            // Assert
            var expected = new List<string>() {
                "\nGarage Vehicles:",
                "         Car LicenseNumber: 0001 Color: Yellow NumberOfWheels: 4     NumberOfCarSeats: 4",
                "    AirPlane LicenseNumber: 0002 Color: Yellow NumberOfWheels: 2     NumberOfAirPlaneWings: 2",
                "  MotorCycle LicenseNumber: 0003 Color: Red    NumberOfWheels: 2     MotorCycleTopSpeed: 250",
                "         Bus LicenseNumber: 0004 Color: Red    NumberOfWheels: 8     BusMaxNumberOfStandingPassengers: 55",
                "        Boat LicenseNumber: 0005 Color: Blue   NumberOfWheels: 0     NumberOfBoatSails: 4",
                "         Car LicenseNumber: 0006 Color: Yellow NumberOfWheels: 4     NumberOfCarSeats: 2",
                "    AirPlane LicenseNumber: 0007 Color: Blue   NumberOfWheels: 2     NumberOfAirPlaneWings: 2",
                "  MotorCycle LicenseNumber: 0008 Color: Red    NumberOfWheels: 2     MotorCycleTopSpeed: 100",
                "         Bus LicenseNumber: 0009 Color: Black  NumberOfWheels: 8     BusMaxNumberOfStandingPassengers: 6",
                "        Boat LicenseNumber: 0010 Color: Blue   NumberOfWheels: 0     NumberOfBoatSails: 2",
                "         Car LicenseNumber: 0011 Color: Green  NumberOfWheels: 4     NumberOfCarSeats: 4",
                "    AirPlane LicenseNumber: 0012 Color: Green  NumberOfWheels: 2     NumberOfAirPlaneWings: 4",
                "  MotorCycle LicenseNumber: 0013 Color: Green  NumberOfWheels: 2     MotorCycleTopSpeed: 100",
                ""
            };

            CollectionAssert.AreEquivalent(expected, Console.ConsoleOutputRows);
        }

        [TestMethod]
        public void ListTypesOfVehicles_13Vehicles_OutputIsMatching()
        {
            // Arrange
            Console = new ConsoleMock();            
            GarageHandler = new GarageHandler();
            UIConsoleHandler consoleHandlerUi = new UIConsoleHandler(Console, GarageHandler);
            GarageHandler.CreateGarage(numberOfVehicles: 13);
            ParkVehicles(VehicleTestData.Vehicles);

            // Act
            consoleHandlerUi.UserListTypesOfVehicles();

            // Assert
            var expected = new List<string>() {
               "\nGarage Vehicle Types:",
                "Typ:Car            Count:3",
                "Typ:AirPlane       Count:3",
                "Typ:MotorCycle     Count:3",
                "Typ:Bus            Count:2",
                "Typ:Boat           Count:2"
            };
            CollectionAssert.AreEquivalent(expected, Console.ConsoleOutputRows);
        }

        [TestMethod]
        public void TryUnparkVehicle_Result_IsTrue()
        {
            // Arrange
            Console = new ConsoleMock();
            GarageHandler = new GarageHandler(/*Console*/);
            GarageHandler.CreateGarage(numberOfVehicles: 13);
            ParkVehicles(VehicleTestData.Vehicles);


            // Act
            var result = GarageHandler.TryUnparkVehicle(VehicleTestData.Car2);

            // Assert
            var expected = true;
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void FindVehicleByLicense_NoneExistingLicenseNumber_ResultIsFalse()
        {
            // Arrange
            Console = new ConsoleMock();
            GarageHandler = new GarageHandler(/*Console*/);
            GarageHandler.CreateGarage(numberOfVehicles: 13);

            ParkVehicles(VehicleTestData.Vehicles);

            // Act
            var nonExistingLicense = "bad license number";
            var vehicle = GarageHandler.FindVehicleByLicense(nonExistingLicense);

            // Assert
            Assert.IsNull(vehicle);
        }
    }
}
