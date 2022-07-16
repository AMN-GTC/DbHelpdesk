using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IVwTicketSummaryServ : IService
    {
        Task<List<VwTicketSummary>> GetList(Specification<VwTicketSummary> specification, CancellationToken cancelationToken = default);
    }
}
