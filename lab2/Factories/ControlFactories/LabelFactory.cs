using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class LabelFactory : ControlFactory
    {
        public override Label CreateControl(PropertyInfo property)
        {
            var label = new Label();
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Text = DisplayNameAttribute.GetDisplayName(property) + ":";
            return label;
        }
    }
}
