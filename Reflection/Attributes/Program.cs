using System;
using System.Reflection;

namespace Attributes
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ExecutablePart();
        }

        private static void ExecutablePart()
        {
            var users = new[]
            {
                new User("Vitaly", 21),
                new User("God"   , 20),
                new User("Mia"   , 16),
            };

            var attribute = GetAgeValidationAttribute();

            foreach (User user in users)
            {
                Console.WriteLine("{0}\t{1}", user.Name, user.Age >= attribute.Age);
            }
        }

        private static AgeValidationAttribute GetAgeValidationAttribute()
        {
            var type = typeof(User);

            return type.GetCustomAttribute<AgeValidationAttribute>();
        }
    }
}