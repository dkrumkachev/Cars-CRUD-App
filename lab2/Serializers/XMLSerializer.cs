using lab2.vehicles;
using lab2.SerializableVehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab2.Serializers
{
    public class XMLSerializer : ISerializer
    {
        

        public void Serialize(string fileName, List<Vehicle> vehicles)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            using TextWriter writer = new StreamWriter(fileName);
            serializer.Serialize(writer, Converter.VehiclesToSerializable(vehicles));
        }

        public List<Vehicle> Deserialize(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            using TextReader reader = new StreamReader(fileName);
            var deserialized = serializer.Deserialize(reader) ?? new List<VehicleSerializableModel>();
            return Converter.SerializableToVehicles((List<VehicleSerializableModel>)deserialized);
        }
    }
}
