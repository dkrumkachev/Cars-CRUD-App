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
    public class PersonSerializableModel
    {
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }

        public PersonSerializableModel() { }

        public PersonSerializableModel(Person person)
        {
            Name = person.Name;
            Age = person.Age;
        }
    }
}
