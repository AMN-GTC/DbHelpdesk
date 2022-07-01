using Helpdesk.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Infrastructure.Configuration
{
    public class EmailStackConfiguration : IEntityTypeConfiguration<EmailStack>
    {
        public void Configure(EntityTypeBuilder<EmailStack> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id)
                .ValueGeneratedOnAdd();

        }

    }
}
