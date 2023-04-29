using System;

namespace lab2.vehicles
{
    public abstract class Vehicle
    {
        protected string manufacturer = string.Empty;
        protected string model = string.Empty;
        protected Person driver = new Person();

        [DisplayName("Manufacturer")]
        public string Manufacturer 
        { 
            get { return manufacturer; } 
            set 
            {
                if (value.Length != 0)
                {
                    manufacturer = value;
                }
            } 
        }
        
        [DisplayName("Model")]
        public string Model 
        { 
            get { return model; } 
            set 
            {
                if (value.Length != 0)
                {
                    model = value;
                }
            } 
        }

        [DisplayName("Driver")]
        public Person Driver { get { return driver; } set { driver = value; } }

        public Vehicle() { }

        public Vehicle(string manufacturer, string model, string name, int age)
        {
            Manufacturer = manufacturer;
            Model = model;
            Driver = new Person(name, age);
        }

        protected Vehicle(Vehicle other)
        {
            this.model = other.Model;
            this.manufacturer = other.Manufacturer;
            this.driver.Age = other.Driver.Age;
            this.driver.Name = other.Driver.Name;
        }

        public abstract Vehicle Clone();

        public override bool Equals(object? obj)
        {
            return obj is Vehicle other && other.Manufacturer == manufacturer && 
                other.Model == model && other.Driver.Equals(driver);
        }
    }
}
