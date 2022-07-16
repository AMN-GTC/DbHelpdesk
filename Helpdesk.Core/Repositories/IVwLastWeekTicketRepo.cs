using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwLastWeekTicketRepo
    {
        Task<List<VwLastWeekTicket>> GetList(Specification<VwLastWeekTicket> specification, CancellationToken cancellationToken = default);
    }
}
