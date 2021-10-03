using System;
using System.Linq;

using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace _6_Logging_providers
{
    internal static class Program : Object
    {
        private static void Main(String[] args)
        {
            // UseLocalLogging();

            // UseGlobalLogging();

            UseIntegratedLogging();
        }

        private static void UseLocalLogging()
        {
            var users = new[]
            {
                new User() { Name = "Andrey", Age = 21 },
                new User() { Name = "Vitaly", Age = 21 },
            };

            using (var context = new ApplicationContext())
            {
                context.GetService<ILoggerFactory>().AddProvider(new CustomLoggerProvider());

                context.AddRange(users);

                context.SaveChanges();

                var deletingUser = context.Users.OrderBy(user => user.Name).First();

                context.Users.Remove(deletingUser);

                context.SaveChanges();
            }
        }

        private static void UseGlobalLogging()
        {
            var users = new[]
            {
                new User() { Name = "Andrey", Age = 21 },
                new User() { Name = "Vitaly", Age = 21 },
            };

            using (var context = new ApplicationContext())
            {
                context.AddRange(users);

                context.SaveChanges();

                var deletingUser = context.Users.OrderBy(user => user.Name).First();

                context.Users.Remove(deletingUser);

                context.SaveChanges();
            }
        }

        private static void UseIntegratedLogging()
        {
            var users = new[]
            {
                new User() { Name = "Andrey", Age = 21 },
                new User() { Name = "Vitaly", Age = 21 },
            };

            using (var context = new ApplicationContext())
            {
                context.AddRange(users);

                context.SaveChanges();

                var deletingUser = context.Users.OrderBy(user => user.Name).First();

                context.Users.Remove(deletingUser);

                context.SaveChanges();
            }
        }
    }
}