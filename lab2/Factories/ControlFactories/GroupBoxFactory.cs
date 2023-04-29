using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class GroupBoxFactory : ControlFactory
    {
        public override GroupBox CreateControl(PropertyInfo property)
        {
            GroupBox groupBox = new GroupBox();
            groupBox.Text = DisplayNameAttribute.GetDisplayName(property);
            return groupBox;
        }

        public override bool CanCreate(Type propertyType)
        {
            return propertyType == typeof(Engine) || propertyType == typeof(Person);
        }
    }
}
