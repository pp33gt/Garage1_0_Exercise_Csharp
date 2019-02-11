using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.BLL.DomainEntities
{
    public partial class Garage<T> : IEnumerable<T> where T : Vehicle
    {
        public int MaxNumberOfGarageVehicles { get; }

        public int NumberOfGarageVehicles => ParkedVehicles.Count;

        internal bool GarageIsFull => NumberOfGarageVehicles >= MaxNumberOfGarageVehicles ? true : false;

        private T[] GarageVehicleArray { get; set; }

        internal List<T> ParkedVehicles
        {
            get { return GarageVehicleArray.Where(a => a != null).ToList(); }
        }

        public Garage(int maxNumberOfGarageVehicles)
        {
            if(maxNumberOfGarageVehicles > 0)
            {
                MaxNumberOfGarageVehicles = maxNumberOfGarageVehicles;
                GarageVehicleArray = new T[MaxNumberOfGarageVehicles];
                return;
            }
            throw new ArgumentException("Error: maxNumberOfGarageVehicles must be greater than -1");
        }

        public bool TryParkVehicle(T vehicle)
        {
            if (ParkedVehicles.Count >= MaxNumberOfGarageVehicles) return false;
            var result = TryAddVehicleToArray(vehicle);
            return result;
        }

        public bool TryUnparkVehicle(T vehicle)
        {
            var result = TryRemoveVehicleFromArray(vehicle);
            return result;
        }

        internal bool TryUnparkVehicle(string license)
        {
            var vehicle = GetVehicleByLicenseNumber(license);
            if(vehicle != null)
            {
                return TryUnparkVehicle(vehicle);
            }
            return false;
        }

        public List<T> ListVehicles()
        {
            return ParkedVehicles;
        }

        public Dictionary<string, int> ListTypesOfVehicles()
        {
            var result = this.GroupBy(a => a.GetType().Name)
                .Select(a => new { Type = a.Key, Count = a.Count() })
                .ToList();

            var res = new Dictionary<string, int>();
            result.ForEach(a => res.Add(a.Type, a.Count));
            return res;
        }

        public T GetVehicleByLicenseNumber(string licenseNumber)
        {
            return ParkedVehicles.Where(vehicle => vehicle.LicenseNumber == licenseNumber).FirstOrDefault();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GarageVehicleArray.Where(a => a != null).ToList().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool TryRemoveVehicleFromArray(T vehicle)
        {
            for (int i = 0; i < MaxNumberOfGarageVehicles; i++)
            {
                if(vehicle == GarageVehicleArray[i])
                {
                    GarageVehicleArray[i] = null;
                    return true;
                }
            }
            return false;
        }

        private bool TryAddVehicleToArray(T vehicle)
        {
            for (int i = 0; i < MaxNumberOfGarageVehicles; i++)
            {
                if (GarageVehicleArray[i] == null)
                {
                    GarageVehicleArray[i] = vehicle;
                    return true;
                }
            }
            return false;
        }
    }
}
