using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lab2.SerializableVehicles
{
    [Serializable]
    public class TruckSerializableModel : MotorVehicleSerializableModel
    {
        public int MaxWeight { get; set; }
        public bool HasTrailer { get; set; }

        public TruckSerializableModel() { }

        public TruckSerializableModel(Truck truck) : base(truck)
        {
            MaxWeight = truck.MaxWeight;
            HasTrailer = truck.HasTrailer;
        }
    }
}
