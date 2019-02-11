//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Runtime.Serialization;
//using System.Runtime.Serialization.Json;
//using System.IO;
//using Garage.DAL.Entities;

//namespace Garage.DAL
//{
//    class JsonSerializer
//    {
//        private const string garageSaveFileNameJson = "GarageSave.json";

//        public string Serialize()
//        {
//            var vehicle = new VehicleJson() { Color = Color.Black, LicenseNumber = "1111", NumberOfWheels = 0 };

//            //Create a stream to serialize the object to.  
//            var ms = new MemoryStream();

//            // Serializer the object to the stream.  
//            var ser = new DataContractJsonSerializer(typeof(VehicleJson));
//            ser.WriteObject(ms, vehicle);
//            byte[] json = ms.ToArray();
//            ms.Close();
//            return Encoding.UTF8.GetString(json, 0, json.Length);
//        }

//        public VehicleJson Deserialize(string json)
//        {
//            var vehicleEntity = new VehicleJson();
//            var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
//            var ser = new DataContractJsonSerializer(vehicleEntity.GetType());
//            vehicleEntity = ser.ReadObject(ms) as VehicleJson;
//            ms.Close();
//            return vehicleEntity;
//        }
//    }
//}
