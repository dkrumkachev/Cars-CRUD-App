using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class TextBoxFactory : ControlFactory
    {
        public override TextBox CreateControl(PropertyInfo property)
        {
            return new TextBox();
        }

        public override void BindToObject(Control control, object obj, PropertyInfo property)
        {
            Bind(control, obj, property.Name, "Text");
        }
    }
}
