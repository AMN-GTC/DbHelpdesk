using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IService
    {
        Dictionary<string, List<string>> GetError();
        bool GetServiceState();
    }
}

    public interface IVwActiveTicketSummaryServ : IService
    {
        Task<List<VwActiveTicketSummary>> GetList(Specification<VwActiveTicketSummary> specification, CancellationToken cancellation = default);
    }
