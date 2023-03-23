using System.ComponentModel;
using System.Runtime.Serialization;

namespace lab2.hierarchy
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
                if (value > 0 && value < MaxWheelsDiameter)
                {
                    wheelsDiameter = value;
                }
            } 
        }
    }
}
