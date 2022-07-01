using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Core;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Core.Common.Mailer
{
    public interface IMailerService
    {

        
        Task<List<Email>> GetUnreadEmail(MailConfig mailConfig);

        Task<bool> SendEmail(MailConfig mailConfig, Email email);

    }
}
