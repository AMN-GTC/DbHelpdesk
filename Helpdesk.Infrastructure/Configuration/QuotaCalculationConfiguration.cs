using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class QuotaCalculationConfiguration : IEntityTypeConfiguration<QuotaCalculation>
    {

        public void Configure(EntityTypeBuilder<QuotaCalculation> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(t => t.Project)
                 .WithMany()
                .HasForeignKey(x => x.ProjectId);
            //.OnDelete(DeleteBehavior.Restrict);
        }
    }
}
