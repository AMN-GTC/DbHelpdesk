using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IStatusService : IService
    {

        public Task<Status> Insert(Status Status, CancellationToken CancellationToken = default);
        public Task<Status> Update(Status Status, int id, CancellationToken cancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Status> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Status>> GetList(Specification<Status> specification, CancellationToken cancellationToken = default);
    }
}
