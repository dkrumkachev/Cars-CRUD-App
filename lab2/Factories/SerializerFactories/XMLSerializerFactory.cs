using lab2.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.SerializerFactories
{
    public class XMLSerializerFactory : SerializerFactory
    {
        public override ISerializer CreateSerializer()
        {
            return new XMLSerializer();
        }

        public override string GetExtension()
        {
            return "XML files|*.xml";
        }
    }
}
