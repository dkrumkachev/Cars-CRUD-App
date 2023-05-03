using lab2.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.SerializerFactories
{
    public abstract class SerializerFactory
    {
        public abstract ISerializer CreateSerializer();

        public abstract string GetExtension();
    }
}
