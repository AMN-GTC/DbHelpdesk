
using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class VwExcelMonthlyConfiguration : IEntityTypeConfiguration<VwExcelMonthly>
    {
        public void Configure(EntityTypeBuilder<VwExcelMonthly> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
