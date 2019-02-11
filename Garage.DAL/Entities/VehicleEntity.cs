using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;


namespace Garage.DAL.Entities
{
    [XmlInclude(typeof(MotorCycleEntity))]
    [XmlInclude(typeof(BoatEntity))]
    [XmlInclude(typeof(CarEntity))]
    [XmlInclude(typeof(AirPlaneEntity))]
    [XmlInclude(typeof(BusEntity))]
    public class VehicleEntity
    {
        public int LicenseNo { get; set; }

        public int NumberOfWheels { get; set; }

        public Color Color { get; set; }
    }
}
