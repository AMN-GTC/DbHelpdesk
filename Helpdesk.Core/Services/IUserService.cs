using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IUserService : IService
    {
        public Task<User> Insert(User User, CancellationToken CancellationToken = default);
        public Task<User> Update(User User, int id, CancellationToken cancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<User> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<User>> GetList(Specification<User> specification, CancellationToken cancellationToken = default);
    }
}

