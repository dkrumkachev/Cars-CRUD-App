using lab2.vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2.SerializableVehicles
{
    public static class Converter
    {
        public static List<VehicleSerializableModel> VehiclesToSerializable(List<Vehicle> vehicles)
        {
            var result = new List<VehicleSerializableModel>();
            foreach (var vehicle in vehicles)
            {
                switch (vehicle)
                {
                    case Bicycle:
                        result.Add(new BicycleSerializableModel((Bicycle)vehicle));
                        break;
                    case Bus:
                        result.Add(new BusSerializableModel((Bus)vehicle));
                        break;
                    case PassengerCar:
                        result.Add(new PassengerCarSerializableModel((PassengerCar)vehicle));
                        break;
                    case Truck:
                        result.Add(new TruckSerializableModel((Truck)vehicle));
                        break;
                    case Motorcycle:
                        result.Add(new MotorcycleSerializableModel((Motorcycle)vehicle));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(vehicle));
                }
            }
            return result;
        }

        public static List<Vehicle> SerializableToVehicles(List<VehicleSerializableModel> serializableModels)
        {
            var result = new List<Vehicle>();
            foreach (var model in serializableModels)
            {
                switch (model)
                {
                    case BicycleSerializableModel:
                        var bicycle = (BicycleSerializableModel)model;
                        result.Add(new Bicycle(bicycle.Type, bicycle.WheelsDiameter, bicycle.Manufacturer, 
                            bicycle.Model, bicycle.Driver.Name, bicycle.Driver.Age));
                        break;
                    case BusSerializableModel:
                        var bus = (BusSerializableModel)model;
                        result.Add(new Bus(bus.Type, bus.RouteNumber, bus.Manufacturer, bus.Model, 
                            bus.Driver.Name, bus.Driver.Age, bus.Engine.Horsepower, bus.Engine.FuelType));
                        break;
                    case MotorcycleSerializableModel:
                        var motorcycle = (MotorcycleSerializableModel)model;
                        result.Add(new Motorcycle(motorcycle.Type, motorcycle.Manufacturer, 
                            motorcycle.Model, motorcycle.Driver.Name, motorcycle.Driver.Age,
                            motorcycle.Engine.Horsepower, motorcycle.Engine.FuelType));
                        break;
                    case PassengerCarSerializableModel:
                        var car = (PassengerCarSerializableModel)model;
                        result.Add(new PassengerCar(car.BodyStyle, car.Drivetrain, car.HasSeatHeating, car.Manufacturer, 
                            car.Model, car.Driver.Name, car.Driver.Age, car.Engine.Horsepower, car.Engine.FuelType));
                        break;
                    case TruckSerializableModel:
                        var truck = (TruckSerializableModel)model;
                        result.Add(new Truck(truck.MaxWeight, truck.HasTrailer, truck.Manufacturer, truck.Model,
                            truck.Driver.Name, truck.Driver.Age, truck.Engine.Horsepower, truck.Engine.FuelType));
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(model));
                }
            }
            return result;
        }
    }
}
