using System;

namespace lab2.hierarchy
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

    }
}
