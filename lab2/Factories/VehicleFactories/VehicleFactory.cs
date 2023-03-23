using lab2.hierarchy;

namespace lab2.Factories.VehicleFactories
{
    public abstract class VehicleFactory
    {
        public abstract Vehicle CreateVehicle();
    }

    public class BicycleFactory : VehicleFactory
    {
        public override Bicycle CreateVehicle()
        {
            return new Bicycle();
        }
    }

    public class MotorcycleFactory : VehicleFactory
    {
        public override Motorcycle CreateVehicle()
        {
            return new Motorcycle();
        }
    }

    public class BusFactory : VehicleFactory
    {
        public override Bus CreateVehicle()
        {
            return new Bus();
        }
    }

    public class TruckFactory : VehicleFactory
    {
        public override Truck CreateVehicle()
        {
            return new Truck();
        }
    }

    public class PassengerCarFactory : VehicleFactory
    {
        public override PassengerCar CreateVehicle()
        {
            return new PassengerCar();
        }
    }
}

