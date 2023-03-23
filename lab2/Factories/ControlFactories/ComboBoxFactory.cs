using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace lab2.Factories.ControlFactories
{
    public class ComboBoxFactory : ControlFactory
    {
        public override void BindToObject(Control control, object obj, PropertyInfo property)
        {
            ((ComboBox)control).DataSource = ((ComboBox)control).Items;
            ((ComboBox)control).DisplayMember = "Name";
            ((ComboBox)control).ValueMember = "Value";
            Bind(control, obj, property.Name, "SelectedValue");
        }

        public override ComboBox CreateControl(PropertyInfo property)
        {
            ComboBox comboBox = new ComboBox();
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            foreach (var value in Enum.GetValues(property.PropertyType))
            {
                var member = property.PropertyType.GetMember(value.ToString())[0];
                string name = DisplayNameAttribute.GetDisplayName(member);
                if (name == string.Empty)
                {
                    name = value.ToString();
                }
                comboBox.Items.Add(new { Name = name, Value = value });
            }
            return comboBox;
        }
    }
}
