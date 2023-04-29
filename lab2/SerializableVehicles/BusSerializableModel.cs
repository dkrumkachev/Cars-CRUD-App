using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static lab2.vehicles.Bus;

namespace lab2.SerializableVehicles
{
    [Serializable]
    public class BusSerializableModel : MotorVehicleSerializableModel
    {
        public BusTypes Type { get; set; }
        public int RouteNumber { get; set; }

        public BusSerializableModel() { }

        public BusSerializableModel(Bus bus) : base(bus) 
        {
            Type = bus.Type;
            RouteNumber = bus.RouteNumber;
        }
    }
}
