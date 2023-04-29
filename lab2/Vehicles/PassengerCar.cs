using static lab2.vehicles.Engine;
using static lab2.vehicles.Motorcycle;

namespace lab2.vehicles
{
    [DisplayName("Passenger car")]
    public class PassengerCar : MotorVehicle
    {
        public enum BodyStyles {
            [DisplayName("Sedan")] Sedan,
            [DisplayName("Hatchback")] Hatchback,
            [DisplayName("Station wagon")] StationWagon,
            [DisplayName("Coupe")] Coupe,
            [DisplayName("Minivan")] Minivan
        };
        public enum Drivetrains {
            [DisplayName("Front-wheel")] FrontWheel,
            [DisplayName("Rear-wheel")] RearWheel,
            [DisplayName("Four-wheel")] FourWheel
        };
        private BodyStyles bodyStyle;
        private Drivetrains drivetrain;
        private bool hasSeatHeating;


        [DisplayName("Body style")]
        public BodyStyles BodyStyle { get { return bodyStyle; } set { bodyStyle = value; } }
        [DisplayName("Drivetrain")]

        public Drivetrains Drivetrain { get { return drivetrain; } set { drivetrain = value; } }
        [DisplayName("Seat heating")]
        public bool HasSeatHeating { get { return hasSeatHeating; } set { hasSeatHeating = value; } }


        public PassengerCar() { }

        public PassengerCar(BodyStyles bodyStyle, Drivetrains drivetrain, bool hasSeatHeating, string manufacturer, 
            string model, string name, int age, int horsepower, FuelTypes fuelType) : 
            base(manufacturer, model, name, age, horsepower, fuelType)
        {
            BodyStyle = bodyStyle;
            Drivetrain = drivetrain;
            HasSeatHeating = hasSeatHeating;
        }

        protected PassengerCar(PassengerCar other) : base(other)
        {
            this.drivetrain = other.Drivetrain;
            this.bodyStyle = other.BodyStyle;
            this.hasSeatHeating = other.HasSeatHeating;
        }

        public override PassengerCar Clone()
        {
            return new PassengerCar(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is PassengerCar other && base.Equals(other) && other.Drivetrain == drivetrain &&
                other.BodyStyle == bodyStyle && other.HasSeatHeating == hasSeatHeating;
        }
    }
}
