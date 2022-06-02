using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class VwExcelReportDetailConfiguration : IEntityTypeConfiguration<VwExcelReportDetail>
    {
        public void Configure(EntityTypeBuilder<VwExcelReportDetail> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
