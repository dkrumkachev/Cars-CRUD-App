namespace Hierarchy
{
    using System.Drawing;

    abstract class Vehicle
    {
        protected string manufacturer;
        protected string model;
        protected Color color;
        protected Person driver;
        protected List<Person> passengers;
    }

    class Bicycle : Vehicle
    {
        private int wheelsDiameter;
        private int weight;
        private int gearsNumber;
    }


    class Motorcycle : Vehicle
    {
        private int engineCapacity;
        private int cylindersNumber;
        private bool hasSidecar;
    }

    abstract class Car : Vehicle
    {
        protected int horsepower;
        protected int wheelsNumber;
        protected bool hasAirConditioning;
        protected bool hasSeatHeating;
    }

    class PassengerCar : Car
    {
        public enum BodyStyle { Sedan, Hatchback, StationWagon, Coupe, Minivan };
        public enum Drivetrain { FrontWheel, RearWheel, FourWheel };
        private BodyStyle bodyStyle;
        private Drivetrain drivetrain;
        private Color interiorColor;
    }

    class Truck : Car
    {
        private int maxWeight;
        private bool hasTrailer;
    }
    class Bus : Car
    {
        private int maxCapacity;
        private int length;
        private int routeNumber;
    }
    class Person
    {
        private string name;
        private int age;
        public enum Gender { Male, Female };
        private Gender gender;
    }
}