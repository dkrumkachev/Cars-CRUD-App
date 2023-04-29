using System.Xml.Linq;
using static lab2.vehicles.Engine;
using static lab2.vehicles.Motorcycle;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab2.vehicles
{
    [DisplayName("Bus")]
    public class Bus : MotorVehicle
    {
        private const int MaxRouteNumber = 1000;
        public enum BusTypes {
            [DisplayName("City")] City,
            [DisplayName("Intercity")] Intercity,
            [DisplayName("Tourist")] Tourist
        };
        private BusTypes type;
        private int routeNumber;

        [DisplayName("Type")]
        public BusTypes Type { get { return type; } set { type = value; } }
        [DisplayName("Route number")]
        public int RouteNumber 
        { 
            get { return routeNumber; } 
            set 
            {
                if (value > 0 && value <= MaxRouteNumber) 
                {
                    routeNumber = value;
                }
            } 
        }

        public Bus() { }

        public Bus(BusTypes type, int routeNumber, string manufacturer, string model, string name, int age, 
            int horsepower, FuelTypes fuelType) : base(manufacturer, model, name, age, horsepower, fuelType)
        {
            Type = type;
            RouteNumber = routeNumber;
        }

        protected Bus(Bus other) : base(other)
        { 
            this.routeNumber = other.RouteNumber;
            this.type = other.Type;
        }

        public override Bus Clone()
        {
            return new Bus(this);
        }

        public override bool Equals(object? obj)
        {
            return obj is Bus other && base.Equals(other) && other.Type == type && other.RouteNumber == routeNumber;
        }

    }
}
