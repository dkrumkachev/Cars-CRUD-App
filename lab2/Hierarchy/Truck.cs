using static lab2.hierarchy.Engine;
using static lab2.hierarchy.Motorcycle;

namespace lab2.hierarchy
{
    [DisplayName("Truck")]
    public class Truck : MotorVehicle
    {
        private const int MaxWeightValue = 50;
        private int maxWeight;
        private bool hasTrailer;

        [DisplayName("Maximum weight")]
        public int MaxWeight
        {
            get { return maxWeight; }
            set
            {
                if (value > 0 && value < MaxWeightValue)
                {
                    maxWeight = value;
                }
            }
        }
        
        [DisplayName("Trailer")]
        public bool HasTrailer { get { return hasTrailer; } set { hasTrailer = value; } }

    }
}
