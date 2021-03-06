using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class VwTicketPICServ : Service<VwTicketPIC>, IVwTicketPICServ
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public VwTicketPICServ(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }


        public Task<List<VwTicketPIC>> GetList(Specification<VwTicketPIC> specification, CancellationToken cancelationToken = default)
        {
            return _helpdeskUnitOfWork.vwTicketPICrepo.GetList(specification, cancelationToken);
        }
    }
}
