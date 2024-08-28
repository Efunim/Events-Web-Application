using Events.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;

namespace Events.Infastructure.Data.Configurations
{
    public class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.Property(e => e.Name).HasMaxLength(100);
            builder.Property(e => e.Description).HasMaxLength(8000);
            builder.Property(e => e.ImagePath).HasMaxLength(1000);
        }
    }
}
