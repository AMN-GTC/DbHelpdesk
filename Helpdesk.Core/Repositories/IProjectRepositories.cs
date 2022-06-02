using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IProjectRepositories
    {
        Task Insert(Project model, CancellationToken cancelationToken = default);
        Task Update(Project model, int id, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<Project> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<Project>> GetList(Specification<Project> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
