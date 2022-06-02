using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IStatusRepositories
    {
        Task Insert(Status model, CancellationToken cancelationToken = default);
        Task Update(Status model, int id, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<Status> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<Status>> GetList(Specification<Status> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
