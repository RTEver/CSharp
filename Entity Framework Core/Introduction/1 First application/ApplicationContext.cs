using Microsoft.EntityFrameworkCore;

namespace _1_First_application
{
    internal sealed class ApplicationContext : DbContext
    {
        internal DbSet<User> Users { get; set; }

        internal ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected sealed override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=PHOENIX; Database=Test_2; Trusted_Connection=True;");
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}