using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Windows.Forms;
using lab2.vehicles;

namespace lab2.Factories.ControlFactories
{
    public abstract class ControlFactory
    {
        public abstract Control CreateControl(PropertyInfo property);

        protected void Bind(Control control, object obj, string vehicleProperty, string controlProperty)
        {
            var binding = new Binding(controlProperty, obj, vehicleProperty);
            binding.DataSourceUpdateMode = DataSourceUpdateMode.OnValidation;
            binding.ReadValue();
            control.DataBindings.Add(binding);
        }

        public virtual bool CanCreate(Type propertyType)
        {
            return false;
        }

        public virtual void BindToObject(Control control, object obj, PropertyInfo property) { }
    }
}
