namespace lab2.hierarchy
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
                if (value > 0 && value < MaxAge)
                {
                    age = value;
                }
            } 
        }
    }
}
