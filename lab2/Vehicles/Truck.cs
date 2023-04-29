using static lab2.vehicles.Engine;
using static lab2.vehicles.Motorcycle;

namespace lab2.vehicles
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
                if (value > 0 && value <= MaxWeightValue)
                {
                    maxWeight = value;
                }
            }
        }
        
        [DisplayName("Trailer")]
        public bool HasTrailer { get { return hasTrailer; } set { hasTrailer = value; } }

        public Truck() { }

        public Truck(int maxWeight, bool hasTrailer, string manufacturer, string model, string name, int age, 
            int horsepower, FuelTypes fuelType) : base(manufacturer, model, name, age, horsepower, fuelType)
        {
            MaxWeight = maxWeight;
            HasTrailer = hasTrailer;
        }

        protected Truck(Truck other) : base(other)
        {
            this.maxWeight = other.MaxWeight;
            this.hasTrailer = other.HasTrailer;
        }


        public override Truck Clone()
        {
            return new Truck(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Truck other && base.Equals(other) && 
                other.MaxWeight == maxWeight && other.HasTrailer == hasTrailer;
        }

    }
}
