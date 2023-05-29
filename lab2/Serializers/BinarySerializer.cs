using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using lab2.SerializableVehicles;

#pragma warning disable SYSLIB0011

namespace lab2.Serializers
{
    public class BinarySerializer : ISerializer
    {
        public void Serialize(Stream stream, List<Vehicle> vehicles)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, Converter.VehiclesToSerializable(vehicles));
        }

        public List<Vehicle> Deserialize(Stream stream)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return Converter.SerializableToVehicles((List<VehicleSerializableModel>)formatter.Deserialize(stream));
        }
    }
}

#pragma warning restore SYSLIB0011
