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
        public void Serialize(string fileName, List<Vehicle> vehicles)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream fileStream = new FileStream(fileName, FileMode.Create);
            formatter.Serialize(fileStream, Converter.VehiclesToSerializable(vehicles));
        }

        public List<Vehicle> Deserialize(string fileName)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using FileStream fileStream = new FileStream(fileName, FileMode.Open);
            return Converter.SerializableToVehicles((List<VehicleSerializableModel>)formatter.Deserialize(fileStream));
        }
    }
}

#pragma warning restore SYSLIB0011
