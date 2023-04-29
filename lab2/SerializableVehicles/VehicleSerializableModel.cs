using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab2.SerializableVehicles
{
    [Serializable]
    [XmlInclude(typeof(BicycleSerializableModel))]
    [XmlInclude(typeof(MotorVehicleSerializableModel))]
    public abstract class VehicleSerializableModel
    {
        public string Manufacturer { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public PersonSerializableModel Driver { get; set; } = new PersonSerializableModel();

        public VehicleSerializableModel() { }

        public VehicleSerializableModel(Vehicle vehicle)
        {
            Manufacturer = vehicle.Manufacturer;
            Model = vehicle.Model;
            Driver = new PersonSerializableModel(vehicle.Driver);
        }
    }
}
