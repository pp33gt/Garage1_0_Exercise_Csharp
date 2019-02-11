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
        public class ListVehicleByPropertyArgs
        {
            public Color? Color { get; set; } = null;
            public string LicenseNumber { get; set; } = null;
            public int? NumberOfWheels { get; set; } = null;

            public int? NumberOfAirPlaneWings { get; set; } = null;
            public int? MotorCycleTopSpeed { get; set; } = null;
            public int? NumberOfCarSeats { get; set; } = null;
            public int? BusMaxNumberOfStandingPassengers { get; set; } = null;
            public int? NumberOfBoatSails { get; set; } = null;
        }

        public List<Vehicle> ListVehicleByProperty(ListVehicleByPropertyArgs listVehicleSearchArgs)
        {
            var vehicleSelection = this.ToList<Vehicle>();
            if (listVehicleSearchArgs.Color.HasValue)
            {
                vehicleSelection = vehicleSelection.Where(a => a.Color == listVehicleSearchArgs.Color).ToList();
            }
            if (listVehicleSearchArgs.LicenseNumber != null)
            {
                vehicleSelection = vehicleSelection.Where(a => a.LicenseNumber == listVehicleSearchArgs.LicenseNumber).ToList();
            }
            if (listVehicleSearchArgs.NumberOfWheels.HasValue)
            {
                vehicleSelection = vehicleSelection.Where(a => a.NumberOfWheels == listVehicleSearchArgs.NumberOfWheels.Value).ToList();
            }
            
            if (listVehicleSearchArgs.NumberOfAirPlaneWings.HasValue)
            {
                vehicleSelection = vehicleSelection.OfType<AirPlane>()
                    .Where(a => a.NumberOfAirPlaneWings == listVehicleSearchArgs.NumberOfAirPlaneWings.Value).ToList<Vehicle>();
            }

            if (listVehicleSearchArgs.MotorCycleTopSpeed.HasValue)
            {
                vehicleSelection = vehicleSelection.OfType<MotorCycle>()
                    .Where(a => a.MotorCycleTopSpeed == listVehicleSearchArgs.MotorCycleTopSpeed.Value).ToList<Vehicle>();
            }
            
            if (listVehicleSearchArgs.NumberOfCarSeats.HasValue)
            {
                vehicleSelection = vehicleSelection.OfType<Car>()
                    .Where(a => a.NumberOfCarSeats == listVehicleSearchArgs.NumberOfCarSeats.Value).ToList<Vehicle>();
            }

            if (listVehicleSearchArgs.BusMaxNumberOfStandingPassengers.HasValue)
            {
                vehicleSelection = vehicleSelection.OfType<Bus>()
                    .Where(a => a.BusMaxNumberOfStandingPassengers == listVehicleSearchArgs.BusMaxNumberOfStandingPassengers.Value).ToList<Vehicle>();
            }
            
            if (listVehicleSearchArgs.NumberOfBoatSails.HasValue)
            {
                vehicleSelection = vehicleSelection.OfType<Boat>()
                    .Where(a => a.NumberOfBoatSails == listVehicleSearchArgs.NumberOfBoatSails.Value).ToList<Vehicle>();
            }
            return vehicleSelection.ToList();
        }
    }
}
