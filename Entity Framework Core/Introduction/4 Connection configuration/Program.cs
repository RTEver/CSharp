using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace _4_Connection_configuration
{
    internal class Program : Object
    {
        private static void Main(String[] args)
        {
            var options = GetOptions();

            using (var context = new ApplicationContext(options))
            {
                var users = context.Users;

                foreach (User user in users)
                {
                    Console.WriteLine($"{user.Id}. {user.Name} - {user.Age}");
                }
            }
        }

        private static DbContextOptions<ApplicationContext> GetOptions()
        {
            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var options = optionsBuilder.UseSqlServer(connectionString).Options;

            return options;
        }
    }
}