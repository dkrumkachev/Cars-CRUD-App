namespace lab2.hierarchy
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
                if (value > 0 && value < MaxHorsepower)
                {
                    horsepower = value;
                }
            } 
        }
    }
}
