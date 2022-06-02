using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwTicketPICRepo
    {
        Task<List<VwTicketPIC>> GetList(Specification<VwTicketPIC> specification, CancellationToken cancellationToken = default);
    }
}
