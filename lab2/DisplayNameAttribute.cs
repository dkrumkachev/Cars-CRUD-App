using System.Reflection;

namespace lab2
{
    internal class DisplayNameAttribute : System.Attribute
    {
        public string Name { get; }
        public DisplayNameAttribute(string name) 
        {
            Name = name;
        }

        public static string GetDisplayName(MemberInfo memberInfo)
        {
            var displayNameAttribute = memberInfo.GetCustomAttribute<DisplayNameAttribute>();
            return displayNameAttribute == null ? string.Empty : displayNameAttribute.Name;
        }
    }
}
