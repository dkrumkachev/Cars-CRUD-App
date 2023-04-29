using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace lab2.SerializableVehicles
{
    [Serializable]
    [XmlInclude(typeof(BusSerializableModel))]
    [XmlInclude(typeof(MotorcycleSerializableModel))]
    [XmlInclude(typeof(PassengerCarSerializableModel))]
    [XmlInclude(typeof(TruckSerializableModel))]
    public abstract class MotorVehicleSerializableModel : VehicleSerializableModel
    {
        public EngineSerializableModel Engine { get; set; } = new EngineSerializableModel();

        public MotorVehicleSerializableModel() { }

        public MotorVehicleSerializableModel(MotorVehicle motorVehicle) : base(motorVehicle)
        {
            Engine = new EngineSerializableModel(motorVehicle.Engine);
        }

    }
}
