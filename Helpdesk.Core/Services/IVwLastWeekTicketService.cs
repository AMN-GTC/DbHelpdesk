using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IVwLastWeekTicketService : IService
    {
        Task<List<VwLastWeekTicket>> GetList(Specification<VwLastWeekTicket> specification, CancellationToken cancelationToken = default);
    }
}
