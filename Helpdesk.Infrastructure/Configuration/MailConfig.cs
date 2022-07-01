using Helpdesk.Core.Entities;
using Helpdesk.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Infrastructure.Configuration
{
    public class MailConfig : IEntityTypeConfiguration<Email>
    {
        public void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.HasKey(f => f.MsgID);
            builder.Property(f => f.MsgID);
            
        } 
        
    }
}
