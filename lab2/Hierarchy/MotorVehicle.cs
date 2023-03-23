using static lab2.hierarchy.Engine;

namespace lab2.hierarchy
{
    public abstract class MotorVehicle : Vehicle
    {
        protected Engine engine = new Engine();

        [DisplayName("Engine")]
        public Engine Engine { get { return engine; } set { engine = value; } }

    }
}
