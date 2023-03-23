using System.ComponentModel;
using static lab2.hierarchy.Engine;

namespace lab2.hierarchy
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
    }
}
