using System;
using System.Linq;

namespace _5_Logging_operations
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            AddTwoUsersThenDeleteOne();
        }

        private static void AddTwoUsersThenDeleteOne()
        {
            var users = new[]
            {
                new User() { Name = "Andrey", Age = 21 },
                new User() { Name = "Vitaly", Age = 21 },
            };

            using (var context = new ApplicationContext())
            {
                context.Users.AddRange(users);

                context.SaveChanges();

                var deletingUser = context.Users.FirstOrDefault();

                context.Users.Remove(deletingUser);

                context.SaveChanges();
            }
        }
    }
}