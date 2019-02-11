using Garage.BLL;
using Garage.BLL.DomainEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Garage.Test
{
    [TestClass]
    public class GarageTest
    {
        private Garage<Vehicle> Garage { get; set; } = null;

        private bool TryParkVehicle<T>(T vehicle) where T: Vehicle
        {
            return Garage.TryParkVehicle(vehicle);
        }

        private List<ParkResult> TryParkVehicles(int numberOfVehicles)
        {
            List<ParkResult> results = new List<ParkResult>();

            if (numberOfVehicles > VehicleTestData.Vehicles.Count)
            {
                throw new System.Exception($"Error: max number of Vehicles is {VehicleTestData.Vehicles.Count}");
            }

            for(var i = 0; i < numberOfVehicles; i++)
            {
                var vehicle = VehicleTestData.Vehicles[i];
                bool parkResult = TryParkVehicle(vehicle);
                var res = new ParkResult() { Result = parkResult, Vehicle = vehicle };
                results.Add(res);
            }
            return results;
        }
       

        [TestMethod]
        public void TryAddVehicle_GarageMax2Add1Car_VehicleCountIs1()
        {
            // Arrange
            Garage = new Garage<Vehicle>(2);

            // Act
            TryParkVehicles(1);

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, Garage.ListVehicles().Count);
        }

        [TestMethod]
        public void TryAddVehicle_GarageMax2AddTwoCars_VehicleCountIs2()
        {
            // Arrange
            Garage = new Garage<Vehicle>(2);

            // Act
            var res = TryParkVehicles(2);

            // Assert
            var expected = 2;
            Assert.AreEqual(expected, Garage.ListVehicles().Count);
        }

        [TestMethod]
        public void TryAddVehicle_GarageMax1AddOneCar_ReturnsTrue()
        {
            // Arrange
            Garage = new Garage<Vehicle>(1);

            // Act
            var res = TryParkVehicles(1);

            // Assert
            var expected = true;
            Assert.AreEqual(expected, res[0].Result);
        }

        [TestMethod]
        public void TryAddVehicle_GarageMax1AddTwoCars_ReturnsFalse()
        {
            // Arrange
            Garage = new Garage<Vehicle>(1);

            // Act
            var result = TryParkVehicles(2);

            // Assert
            var expected = false;
            Assert.AreEqual(expected, result[result.Count-1].Result);
        }


        [TestMethod]
        public void TryUnparkVehicle_GarageMax2TwoCars_ReturnsTrue()
        {
            // Arrange
            Garage = new Garage<Vehicle>(2);
            TryParkVehicles(2);

            // Act
            var resultRemove = Garage.TryUnparkVehicle(VehicleTestData.Car1);

            // Assert
            var expected = true;
            Assert.AreEqual(expected, resultRemove);
        }

        [TestMethod]
        public void TryUnparkVehicle_GarageMax2TwoCars_CountIsOne()
        {
            // Arrange
            Garage = new Garage<Vehicle>(2);
            TryParkVehicles(2);

            // Act
            var resultRemove = Garage.TryUnparkVehicle(VehicleTestData.Car1);

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, Garage.ListVehicles().Count);
        }

        [TestMethod]
        public void TryAddVehicle_GarageMax5AllVehicleTypes_CountIs5()
        {
            // Arrange
            Garage = new Garage<Vehicle>(5);

            // Act
            TryParkVehicles(5);

            // Assert
            var expected = 5;
            Assert.AreEqual(expected, Garage.ListVehicles().Count);
        }


        [TestMethod]
        public void ListVehicleByProperty_ColorIsBlue_CountIs2()
        {
            // Arrange
            Garage = new Garage<Vehicle>(10);

            TryParkVehicles(10);

            // Act
            var blueVehicles = Garage.ListVehicleByProperty(new Garage<Vehicle>.ListVehicleByPropertyArgs() { Color = Color.Blue });

            // Assert
            var expected = 3;
            Assert.AreEqual(expected, blueVehicles.Count);
        }

        [TestMethod]
        public void ListVehicleByProperty_ColorIsBlueAndLicense0002_CountIs1()
        {
            // Arrange
            Garage = new Garage<Vehicle>(10);

            TryParkVehicles(10);

            // Act
            var blueVehicles = Garage.ListVehicleByProperty(new Garage<Vehicle>.ListVehicleByPropertyArgs()
            {
                Color = Color.Blue,
                LicenseNumber = VehicleTestData.AirPlane2.LicenseNumber
            });

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, blueVehicles.Count);
        }

        [TestMethod]
        public void ListVehicleByProperty_ColorIsBlueAndNumberOfAirPlaneWings_CountIs1()
        {
            // Arrange
            Garage = new Garage<Vehicle>(10);

            TryParkVehicles(10);

            // Act
            var blueVehicles = Garage.ListVehicleByProperty(new Garage<Vehicle>.ListVehicleByPropertyArgs()
            {
                Color = Color.Blue,
                NumberOfAirPlaneWings = VehicleTestData.AirPlane2.NumberOfAirPlaneWings
            });

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, blueVehicles.Count);
        }

        [TestMethod]
        public void ListVehicleByProperty_NumberOfWheels2AndMotorCycleTopSpeed250_CountIs1()
        {
            // Arrange
            Garage = new Garage<Vehicle>(10);

            TryParkVehicles(10);

            // Act
            var blueVehicles = Garage.ListVehicleByProperty(new Garage<Vehicle>.ListVehicleByPropertyArgs()
            {
                NumberOfWheels = 2,
                MotorCycleTopSpeed = 250,
            });

            // Assert
            var expected = 1;
            Assert.AreEqual(expected, blueVehicles.Count);
        }

    }
}
