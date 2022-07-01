using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IConversationService : IService
    {
        //public Task SendEmailAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public Task<Conversation> Insert(Conversation conversation, CancellationToken CancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Conversation> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancellationToken = default);
        public Task<Conversation> SendConversation(Conversation conversation, CancellationToken cancellationToken = default);
        public EmailStack BindStackEmailToConversation(Email email);
        public Task<bool> GetConversationFromEmail(CancellationToken cancellationToken = default);
        public Task<List<Conversation>> GetConversations(Specification<Conversation> specification, CancellationToken cancellationToken);
        public Task<List<Conversation>> GetConversationTicketId(int ticketId, CancellationToken cancellationToken);

    }
}
