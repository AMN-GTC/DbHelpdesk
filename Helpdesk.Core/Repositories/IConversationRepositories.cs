using API_DB_Conversation.Entity;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API_DB_Conversation.Repositories
{
    public interface IConversationRepositories
    {
        Task Insert(Conversation model, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<Conversation> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
