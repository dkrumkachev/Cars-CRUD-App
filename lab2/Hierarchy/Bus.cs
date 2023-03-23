using static lab2.hierarchy.Engine;
using static lab2.hierarchy.Motorcycle;

namespace lab2.hierarchy
{
    [DisplayName("Bus")]
    public class Bus : MotorVehicle
    {
        private const int MaxRouteNumber = 200;
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
                if (value > 0 && value < MaxRouteNumber) 
                {
                    routeNumber = value;
                }
            } 
        }

    }
}
