namespace lab2.vehicles
{
    public class Engine
    {
        private const int MaxHorsepower = 3000;
        public enum FuelTypes {
            [DisplayName("Gasoline")] Gasoline,
            [DisplayName("Diesel")] Diesel,
            [DisplayName("Electric")] Electric
        };
        private FuelTypes fuelType;
        private int horsepower;

        [DisplayName("Fuel type")]
        public FuelTypes FuelType { get { return fuelType; } set { fuelType = value; } }
        [DisplayName("Power (hp)")]
        public int Horsepower 
        { 
            get { return horsepower; } 
            set 
            {
                if (value > 0 && value <= MaxHorsepower)
                {
                    horsepower = value;
                }
            } 
        }

        public Engine() { }

        public Engine(int horsepower, FuelTypes fuelType) 
        {
            Horsepower = horsepower;
            FuelType = fuelType;
        }

        public override bool Equals(object? obj)
        {
            return obj is Engine other && other.FuelType == fuelType && other.Horsepower == horsepower;
        }
    }
}
