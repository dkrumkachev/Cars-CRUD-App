using static lab2.vehicles.Engine;

namespace lab2.vehicles
{
    public abstract class MotorVehicle : Vehicle
    {
        protected Engine engine = new Engine();

        [DisplayName("Engine")]
        public Engine Engine { get { return engine; } set { engine = value; } }

        public MotorVehicle() { }

        public MotorVehicle(string manufacturer, string model, string name, int age, int horsepower, FuelTypes fuelType) :
            base(manufacturer, model, name, age)
        {
            engine = new Engine(horsepower, fuelType);
        }

        protected MotorVehicle(MotorVehicle other) : base(other) 
        {
            this.engine.Horsepower = other.Engine.Horsepower;
            this.engine.FuelType = other.Engine.FuelType;
        }

        public override bool Equals(object? obj)
        {
            return obj is MotorVehicle other && base.Equals(other) && other.Engine.Equals(engine);
        }

    }
}
