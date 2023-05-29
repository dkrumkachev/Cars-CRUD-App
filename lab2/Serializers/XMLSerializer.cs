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
        public void Serialize(Stream stream, List<Vehicle> vehicles)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            serializer.Serialize(stream, Converter.VehiclesToSerializable(vehicles));
        }

        public List<Vehicle> Deserialize(Stream stream)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<VehicleSerializableModel>));
            var deserialized = serializer.Deserialize(stream) ?? new List<VehicleSerializableModel>();
            return Converter.SerializableToVehicles((List<VehicleSerializableModel>)deserialized);
        }
    }
}
