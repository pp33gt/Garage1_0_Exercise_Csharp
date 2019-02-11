using Garage.Common;
using System;
using System.Collections.Generic;
using Garage.BLL.DomainEntities;
using System.IO;
using System.Xml.Serialization;
using Garage.DAL.Entities;
using Garage.DAL;
using System.Linq;

namespace Garage.BLL
{
    public partial class GarageHandler
    {
        internal Garage<Vehicle> Garage { get; set; } = null;
        internal bool GarageExists => Garage != null ? true : false;

        public GarageHandler()
        {
        }

        internal void CreateGarage(int numberOfVehicles)
        {
            if (numberOfVehicles < 0)
            {
                throw new Exception("numberOfVehicles must be posistive number");
            }
            Garage = new Garage<Vehicle>(numberOfVehicles);
        }

        internal List<Vehicle> ListVehicles()
        {
            return Garage.ListVehicles();
        }

        internal Dictionary<string, int> ListTypesOfVehicles()
        {
            return Garage.ListTypesOfVehicles();
        }

        internal Vehicle GetRandomVehicle()
        {
            return VehicleFactory.GetRandomVehicle();
        }

        internal bool TryParkVehicle(Vehicle vehicle)
        {
            return Garage.TryParkVehicle(vehicle);
        }

        internal Vehicle FindVehicleByLicense(string licenseNumber)
        {
            return Garage.GetVehicleByLicenseNumber(licenseNumber);
        }

        internal List<Vehicle> FindVehiclesByProperty(Garage<Vehicle>.ListVehicleByPropertyArgs listVehicleSearchArgs)
        {
            return Garage.ListVehicleByProperty(listVehicleSearchArgs);
        }

        internal bool TryUnparkVehicle(Vehicle vehicle)
        {
            return Garage.TryUnparkVehicle(vehicle);
        }

        internal void SaveGarage()
        {
            var xmlHandler = new XmlHandler();
            var vehiclesToSave = DalEntityMapper.VehicleToDalObjects(Garage.ParkedVehicles);
            xmlHandler.SaveGarage(Garage.MaxNumberOfGarageVehicles, vehiclesToSave);
        }

        internal void LoadGarage()
        {
            var xmlHandler = new XmlHandler();

            GarageSave garageSave;
            try
            {
                garageSave = xmlHandler.LoadGarage();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var garage = new Garage<Vehicle>(garageSave.MaxNumberOfGarageVehicles);
            var vehicles = DalEntityMapper.DalObjectsToVehicles(garageSave.Vehicles);
            var highestLicenseNumber = 0;
            vehicles.ForEach(
                a =>
                {
                    if (highestLicenseNumber <= a.LicenseNo)
                    {
                        highestLicenseNumber = a.LicenseNo;
                    }
                    garage.TryParkVehicle(a);
                });
            VehicleFactory.SetLicenseNumber = ++highestLicenseNumber;

            Garage = garage;
        }
    }
}
