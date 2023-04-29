using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static lab2.vehicles.Motorcycle;

namespace lab2.SerializableVehicles
{
    [Serializable]
    public class MotorcycleSerializableModel : MotorVehicleSerializableModel
    {
        public MotorcycleTypes Type { get; set; }

        public MotorcycleSerializableModel() { }

        public MotorcycleSerializableModel(Motorcycle motorcycle) : base(motorcycle) 
        {
            Type = motorcycle.Type;
        }
    }
}
