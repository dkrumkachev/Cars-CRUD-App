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
    public class EngineSerializableModel
    {
        public Engine.FuelTypes FuelType { get; set; }
        public int Horsepower { get; set; }

        public EngineSerializableModel() { }

        public EngineSerializableModel(Engine engine) 
        {
            FuelType = engine.FuelType;
            Horsepower = engine.Horsepower;
        }
        
    }
}
