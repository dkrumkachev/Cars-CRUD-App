using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class CheckBoxFactory : ControlFactory
    {
        public override CheckBox CreateControl(PropertyInfo property)
        {
            return new CheckBox();
        }

        public override void BindToObject(Control control, object obj, PropertyInfo property)
        {
            Bind(control, obj, property.Name, "Checked");
        }
    }
}
