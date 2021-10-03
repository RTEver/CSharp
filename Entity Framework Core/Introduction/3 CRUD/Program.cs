using System;
using System.Linq;

namespace _3_CRUD
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            ReadUsers();

            AddUser(new User() { Name = "Vitaly", Age = 21 });
            AddUser(new User() { Name = "Andrey", Age = 21 });

            ReadUsers();

            var firstUser = GetFirstUser();

            UpdateUser(firstUser, "Denis", 20);

            ReadUsers();

            firstUser = GetFirstUser();

            DeleteUser(firstUser);

            ReadUsers();
        }

        private static User GetFirstUser()
        {
            using (var context = new ApplicationContext())
            {
                return context.Users.FirstOrDefault();
            }
        }

        private static User GetLastUser()
        {
            using (var context = new ApplicationContext())
            {
                return context.Users.LastOrDefault();
            }
        }

        private static void AddUser(User user)
        {
            using (var context = new ApplicationContext())
            {
                context.Users.Add(user);

                context.SaveChanges();
            }
        }

        private static void DeleteUser(User user)
        {
            using (var context = new ApplicationContext())
            {
                context.Users.Remove(user);

                context.SaveChanges();
            }
        }

        private static void UpdateUser(User user, String name, Int32 age)
        {
            using (var context = new ApplicationContext())
            {
                user.Name = name;
                user.Age = age;

                context.Users.Update(user);

                context.SaveChanges();
            }
        }

        private static void ReadUsers()
        {
            using (var context = new ApplicationContext())
            {
                var users = context.Users.ToList();

                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
                }

                Console.WriteLine();
            }
        }
    }
}