using Microsoft.EntityFrameworkCore;
using Events.Application;

namespace Events.Infastructure.Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<>
    }
}
