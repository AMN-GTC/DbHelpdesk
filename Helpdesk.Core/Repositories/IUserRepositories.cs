using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IUserRepositories
    {
        Task Insert(User model, CancellationToken cancelationToken = default);
        Task Update(User model, int id, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<User> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<User>> GetList(Specification<User> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
