using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface ITicketRepositories
    {
        Task Insert(Ticket model, CancellationToken cancelationToken = default);
        Task Update(Ticket model, int id, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<Ticket> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<Ticket>> GetList(Specification<Ticket> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
