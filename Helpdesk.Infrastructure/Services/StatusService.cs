using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class StatusService : Service<Status>, IStatusService
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public StatusService(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Status>> GetList(Specification<Status> specification, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Status.GetList(specification, cancellationToken);
        }

        public Task<Status> GetObject(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Status> Insert(Status Status, CancellationToken CancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<Status> Update(Status Status, int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
