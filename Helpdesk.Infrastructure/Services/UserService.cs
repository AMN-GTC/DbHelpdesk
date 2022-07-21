using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class UserService : Service<User>, IUserService
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public UserService(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.User.Delete(id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public Task<List<User>> GetList(Specification<User> specification, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.User.GetList(specification, cancellationToken);

        }

        public Task<User> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.User.GetObject(id, cancellationToken);

        }
        public async Task<User> Insert(User User, CancellationToken CancellationToken = default)
        {
            await _helpdeskUnitOfWork.User.Insert(User, CancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(CancellationToken);
            return User;
        }

        public async Task<User> Update(User User, int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.User.Update(User, id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return User;
        }
    }
}
