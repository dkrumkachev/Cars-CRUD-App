using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static lab2.vehicles.Bicycle;

namespace lab2.SerializableVehicles
{
    [Serializable]
    public class BicycleSerializableModel : VehicleSerializableModel
    {
        public BikeTypes Type { get; set; }
        public int WheelsDiameter { get; set; }

        public BicycleSerializableModel() { }

        public BicycleSerializableModel(Bicycle bicycle) : base(bicycle)
        {
            Type = bicycle.Type;
            WheelsDiameter = bicycle.WheelsDiameter;
        }
    }
}
