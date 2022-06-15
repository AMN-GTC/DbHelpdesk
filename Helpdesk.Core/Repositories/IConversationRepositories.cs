using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
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
