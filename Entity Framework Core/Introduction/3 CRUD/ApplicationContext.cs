using Microsoft.EntityFrameworkCore;

namespace _3_CRUD
{
    internal sealed class ApplicationContext : DbContext
    {
        internal DbSet<User> Users { get; set; }

        internal ApplicationContext()
            : base()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=helloappdb;Trusted_Connection=True;");
        }
    }
}