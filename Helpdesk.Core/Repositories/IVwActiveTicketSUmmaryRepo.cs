using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwActiveTicketSummaryRepo
    {
        Task<List<VwActiveTicketSummary>> GetList(Specification<VwActiveTicketSummary> specification, CancellationToken cancellationToken = default);
    }
}
