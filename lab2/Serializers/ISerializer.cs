using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Serializers
{
    public interface ISerializer
    {
        void Serialize(string filename, List<Vehicle> vehicles);

        List<Vehicle> Deserialize(string filename);

    }
}
