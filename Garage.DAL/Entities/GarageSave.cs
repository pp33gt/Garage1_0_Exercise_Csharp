using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Garage.DAL.Entities
{
    public class GarageSave
    {
        public int MaxNumberOfGarageVehicles { get; set; }
        public List<VehicleEntity> Vehicles { get; set; }
    }
}
