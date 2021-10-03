using Microsoft.EntityFrameworkCore;

namespace _4_Connection_configuration
{
    internal sealed class ApplicationContext : DbContext
    {
        internal DbSet<User> Users { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}