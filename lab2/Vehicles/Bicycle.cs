using System.ComponentModel;
using System.Runtime.Serialization;
using static lab2.vehicles.Engine;

namespace lab2.vehicles
{
    [DisplayName("Bicycle")]
    public class Bicycle : Vehicle
    {
        private const int MaxWheelsDiameter = 30;
        public enum BikeTypes {
            [DisplayName("Road")] Road,
            [DisplayName("City")] City,
            [DisplayName("Mountain")] Mountain,
            [DisplayName("Racing")] Racing
        };
        private BikeTypes type;
        private int wheelsDiameter;

        [DisplayName("Type")]
        public BikeTypes Type { get { return type; } set { type = value; } }
        [DisplayName("Wheels diameter")]
        public int WheelsDiameter
        {
            get { return wheelsDiameter; }
            set
            {
                if (value > 0 && value <= MaxWheelsDiameter)
                {
                    wheelsDiameter = value;
                }
            }
        }

        public Bicycle() { }

        public Bicycle(BikeTypes type, int wheelsDiameter, string manufacturer, string model, string name, int age) : 
            base(manufacturer, model, name, age)
        {
            Type = type;
            WheelsDiameter = wheelsDiameter;
        }

        protected Bicycle(Bicycle other) : base(other)
        {
            this.type = other.Type;
            this.wheelsDiameter = other.WheelsDiameter;
        }

        public override Bicycle Clone()
        {
            return new Bicycle(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Bicycle other && base.Equals(other) && 
                other.Type == type && other.WheelsDiameter == wheelsDiameter;
        }
    }
}
