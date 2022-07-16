using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{

    public interface ITicketService : IService
    {
        public Task<Ticket> Insert(Ticket ticket, CancellationToken CancellationToken = default);
        public Task<Ticket> Update(Ticket ticket, int id, CancellationToken cancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Ticket> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Ticket>> GetList(Specification<Ticket> specification, CancellationToken cancellationToken = default);
    }
}
