using static lab2.hierarchy.Engine;
using static lab2.hierarchy.Motorcycle;

namespace lab2.hierarchy
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

    }
}
