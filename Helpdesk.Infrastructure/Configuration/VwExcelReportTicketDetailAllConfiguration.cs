using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class VwExcelReportTicketDetailAllConfiguration : IEntityTypeConfiguration<VwExcelReportTicketDetailAll>
    {

        public void Configure(EntityTypeBuilder<VwExcelReportTicketDetailAll> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
             .ValueGeneratedOnAdd();

        }
    }
}
