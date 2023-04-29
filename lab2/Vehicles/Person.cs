namespace lab2.vehicles
{
    public class Person
    {
        private const int MaxAge = 120;
        private string name = string.Empty;
        private int age;

        [DisplayName("Name")]
        public string Name 
        { 
            get { return name; } 
            set 
            {
                if (value.Length != 0)
                {
                    name = value;
                }
            } 
        }

        [DisplayName("Age")]
        public int Age 
        { 
            get { return age; }
            set
            {
                if (value > 0 && value <= MaxAge)
                {
                    age = value;
                }
            } 
        }

        public Person() { }

        public Person(string name, int age) 
        {
            Name = name;
            Age = age;
        }

        public override bool Equals(object? obj)
        {
            return obj is Person other && other.Name == name && other.Age == age;
        }
    }
}
