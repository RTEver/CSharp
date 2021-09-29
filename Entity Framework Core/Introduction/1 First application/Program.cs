using System;
using System.Linq;

namespace _1_First_application
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            using (var context = new ApplicationContext())
            {
                // Create two User objects.
                var user_1 = new User { Name = "Tom", Age = 33 };
                var user_2 = new User { Name = "Alice", Age = 26 };

                // Add this users in our database.
                context.Users.Add(user_1);
                context.Users.Add(user_2);

                context.SaveChanges();

                Console.WriteLine("Objects save succesfully.");

                // Get users from database and output them on console.
                var users = context.Users.ToList();

                Console.WriteLine("User list:");

                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
                }
            }
        }
    }
}