using Garage.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Garage.DAL
{
    public class XmlHandler
    {
        private const string garageSaveFileName = "GarageSave.xml";

        private XmlSerializer serializer = new XmlSerializer(typeof(GarageSave));

        public GarageSave LoadGarage()
        {
            var garageSave = new GarageSave();

            using (var reader = new StreamReader(garageSaveFileName))
            {
                garageSave = (GarageSave)serializer.Deserialize(reader);
            }
            return garageSave;

            //var jsonSerializer = new JsonSerializer();
            //var data = jsonSerializer.Serialize();
            //var vehicle = jsonSerializer.Deserialize(data);
            //return null

            //var filePath = string.Empty;
            //using (FileStream fs = new FileStream(filePath, FileMode.Open))
            //{
            //    var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            //    var vehicles =(GarageSave) binaryFormatter.Deserialize(fs);
            //}
        }

        public void SaveGarage(int maxNumberOfGarageVehicles, List<VehicleEntity> vehicles)
        {
            var garageSave = new GarageSave();
            garageSave.MaxNumberOfGarageVehicles = maxNumberOfGarageVehicles;
            garageSave.Vehicles = vehicles;

            var serializer = new XmlSerializer(typeof(GarageSave));
            {
                using (var writer = new StreamWriter(garageSaveFileName))
                {
                    serializer.Serialize(writer, garageSave);
                }
            }
        }
    }
}
