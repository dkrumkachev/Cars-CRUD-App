using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static lab2.vehicles.PassengerCar;

namespace lab2.SerializableVehicles
{
    [Serializable]
    public class PassengerCarSerializableModel : MotorVehicleSerializableModel
    {
        public BodyStyles BodyStyle { get; set; }
        public Drivetrains Drivetrain { get; set; }
        public bool HasSeatHeating { get; set; }

        public PassengerCarSerializableModel() { }

        public PassengerCarSerializableModel(PassengerCar passengerCar) : base(passengerCar)
        {
            BodyStyle = passengerCar.BodyStyle;
            Drivetrain = passengerCar.Drivetrain;
            HasSeatHeating = passengerCar.HasSeatHeating;
        }
    }
}
