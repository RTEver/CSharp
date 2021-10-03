using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace _6_Logging_providers
{
    internal sealed class ApplicationContext : DbContext
    {
        public static readonly ILoggerFactory CustomLoggerFactory =
            // LoggerFactory.Create(builder => builder.AddProvider(new CustomLoggerProvider()));
            LoggerFactory.Create(builder =>
            {
                builder
                    .AddFilter((category, level) => level == LogLevel.Information)
                    // .AddProvider(new CustomLoggerProvider());
                    .AddConsole();
            });

        public DbSet<User> Users { get; set; }

        public ApplicationContext()
            : base()
        {
            Database.EnsureDeleted();

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");

            optionsBuilder.UseLoggerFactory(CustomLoggerFactory);
        }
    }
}