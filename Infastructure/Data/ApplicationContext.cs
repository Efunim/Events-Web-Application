using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Events.Infastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options) 
        {

        }

        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streetes { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Participant> Participants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var location = modelBuilder.Entity<Location>();
            location.Property(l => l.Name).HasMaxLength(100);
            location.Property(l => l.House).HasMaxLength(10);

            var eventCategory = modelBuilder.Entity<EventCategory>();
            eventCategory.Property(e => e.Name).HasMaxLength(50);

            var @event = modelBuilder.Entity<Event>();
            @event.Property(e => e.Name).HasMaxLength(100);
            @event.Property(e => e.Description).HasMaxLength(8000);
            @event.Property(e => e.ImagePath).HasMaxLength(1000);

            var user = modelBuilder.Entity<User>();
            user.Property(u => u.Name).HasMaxLength(100);
            user.Property(u => u.Surname).HasMaxLength(100);
            user.Property(u => u.Email).HasMaxLength(254);
            user.Property(u => u.PasswordHash).HasMaxLength(64);
            user.HasIndex(u => u.Email).IsUnique();
        }
    }
}
