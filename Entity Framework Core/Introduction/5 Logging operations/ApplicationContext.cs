using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace _5_Logging_operations
{
    internal sealed class ApplicationContext : DbContext
    {
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
            
            // First way --> LogTo with action delegate (void)(String)
            // optionsBuilder.LogTo(System.Console.WriteLine, new[] { RelationalEventId.CommandExecuted });
            optionsBuilder.LogTo(System.Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
        }
    }
}