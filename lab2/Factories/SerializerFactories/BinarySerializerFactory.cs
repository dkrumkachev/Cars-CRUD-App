using lab2.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.SerializerFactories
{
    public class BinarySerializerFactory : SerializerFactory
    {
        public override ISerializer CreateSerializer()
        {
            return new BinarySerializer();
        }

        public override string GetExtension()
        {
            return "Binary files|*.bin";
        }
    }
}
