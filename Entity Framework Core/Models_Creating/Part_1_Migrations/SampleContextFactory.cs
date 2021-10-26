using System;
using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

using Microsoft.Extensions.Configuration;

namespace Part_1_Migrations
{
    public sealed class SampleContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(String[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();

            var builder = new ConfigurationBuilder();

            builder.SetBasePath(Directory.GetCurrentDirectory());

            builder.AddJsonFile("appsettings.json");

            var config = builder.Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString, options =>
            {
                options.CommandTimeout((Int32)TimeSpan.FromMinutes(10).TotalSeconds);
            });

            return new ApplicationContext(optionsBuilder.Options);
        }
    }
}