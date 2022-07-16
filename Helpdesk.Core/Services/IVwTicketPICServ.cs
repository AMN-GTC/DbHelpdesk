using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IVwTicketPICServ : IService
    {
        Task<List<VwTicketPIC>> GetList(Specification<VwTicketPIC> specification, CancellationToken cancelationToken = default);
    }
}
