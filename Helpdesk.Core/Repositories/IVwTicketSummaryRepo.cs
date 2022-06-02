using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwTicketSummaryRepo
    {
        Task<List<VwTicketSummary>> GetList(Specification<VwTicketSummary> specification, CancellationToken cancellationToken = default);
    }
}
