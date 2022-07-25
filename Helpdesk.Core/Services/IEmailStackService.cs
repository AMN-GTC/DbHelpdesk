using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Services
{
    public interface IServiceEmailStack
    {
        Dictionary<string, List<string>> GetError();

        Boolean GetServiceState();
    }
    public interface IEmailStackService : IServiceEmailStack
    {
        public Task<EmailStack> Insert(EmailStack email, CancellationToken cancellationToken = default);

        public Task<EmailStack> Update(int id, EmailStack emailStack, CancellationToken cancellationToken = default);

        public Task<EmailStack> GetObject(int id, CancellationToken cancellationToken = default);

        public Task<List<EmailStack>> GetList(Specification<EmailStack> specification, CancellationToken cancellationToken = default);

        public Task <bool> GetUnreadEmail (CancellationToken cancellationToken = default);
        
        List<EmailStack> ConvertListEmailToListEmailStack(List<Email> mails);

        List<Ticket> TicketToEmail(List<EmailStack> emailStack);

        Ticket TicketToEmail2(EmailStack emailStack);
        public Task<bool> TicketMaker(EmailStack emailStack,Project projectInfo, CancellationToken cancellationToken = default);

        public Task<bool> TicketMakers(List <EmailStack> emailStacks, CancellationToken cancellationToken = default);
        }
    }
