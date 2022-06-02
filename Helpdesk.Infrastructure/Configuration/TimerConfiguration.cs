using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class TimerConfiguration : IEntityTypeConfiguration<TimerEntity>
    {
        public void Configure(EntityTypeBuilder<TimerEntity> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                  .ValueGeneratedOnAdd();

            builder.HasOne(x => x.Ticket)
                  .WithMany(x => x.Timers)
                  .HasForeignKey(x => x.TicketId)
                  .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
