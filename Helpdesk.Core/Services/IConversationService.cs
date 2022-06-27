using API_DB_Ticket.Entity;
using API_DB_Ticket.Settings;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API_DB_Ticket.Services
{
    public interface IConversationService : IService
    {
        
       
        public Task SendEmailAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public Task<Conversation> Insert(Conversation conversation, CancellationToken CancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Conversation> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancellationToken = default);
        public Task<Conversation> SendConversation(Conversation conversation, CancellationToken cancellationToken = default);
        public EmailStack BindStackEmailToConversation(Email email);
        public Task<bool> GetConversationFromEmail(CancellationToken cancellationToken = default);
        public Task<IReadOnlyList<Conversation>> GetConversations(Specification<Conversation> filter);



    }
}
