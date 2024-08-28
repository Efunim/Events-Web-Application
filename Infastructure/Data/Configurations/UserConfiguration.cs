using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Events.Infastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Name).HasMaxLength(100);
            builder.Property(u => u.Surname).HasMaxLength(100);
            builder.Property(u => u.Email).HasMaxLength(254);
            builder.Property(u => u.PasswordHash).HasMaxLength(64);
            builder.HasIndex(u => u.Email).IsUnique();
        }
    }
}
