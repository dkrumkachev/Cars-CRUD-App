using System.ComponentModel;
using static lab2.vehicles.Engine;

namespace lab2.vehicles
{
    [DisplayName("Motorcycle")]
    public class Motorcycle : MotorVehicle
    {
        public enum MotorcycleTypes {
            [DisplayName("Standart")] Standart,
            [DisplayName("Cruiser")] Cruiser,
            [DisplayName("Sports")] Sports,
            [DisplayName("Off-road")] OffRoad,
            [DisplayName("Touring")] Touring,
            [DisplayName("Scooter")] Scooter,
            [DisplayName("Moped")] Moped
        };
        private MotorcycleTypes type;

        [DisplayName("Type")]
        public MotorcycleTypes Type { get { return type; } set { type = value; } }

        public Motorcycle() { }

        public Motorcycle(MotorcycleTypes type, string manufacturer, string model, string name, int age,
            int horsepower, FuelTypes fuelType) : base(manufacturer, model, name, age, horsepower, fuelType)
        {
            Type = type;
        }

        protected Motorcycle(Motorcycle other) : base(other) 
        { 
            this.type = other.Type;
        }

        public override Motorcycle Clone()
        {
            return new Motorcycle(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Motorcycle other && base.Equals(other) && other.Type == type;
        }
    }
}
