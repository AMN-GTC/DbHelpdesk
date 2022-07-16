using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class VwQuotaServ : Service<VwQuota>, IVwQuotaServ 
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public VwQuotaServ(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public Task<List<VwQuota>> GetList(Specification<VwQuota> specification, CancellationToken cancellation = default)
        {
            return _helpdeskUnitOfWork.vwQuotaRepo.GetList(specification, cancellation);
        }
    }
}
