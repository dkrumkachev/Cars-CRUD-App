using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class NumericUpDownFactory : ControlFactory
    {
        public override CustomNumericUpDown CreateControl(PropertyInfo property)
        {
            var numericUpDown = new CustomNumericUpDown();
            numericUpDown.Maximum = 5000;
            numericUpDown.Minimum = 0;
            numericUpDown.Text = "";
            return numericUpDown;
        }

        public override void BindToObject(Control control, object obj, PropertyInfo property)
        {
            Bind(control, obj, property.Name, "Value");
        }

        public override bool CanCreate(Type propertyType)
        {
            return propertyType == typeof(int);
        }
    }
}
