using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class VwTicketSummaryServ : Service<VwTicketSummary>, IVwTicketSummaryServ
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public VwTicketSummaryServ(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public Task<List<VwTicketSummary>> GetList(Specification<VwTicketSummary> specification, CancellationToken cancelationToken = default)
        {
            return _helpdeskUnitOfWork.vwTicketSummaryrepo.GetList(specification, cancelationToken);
        }
    }
}
