using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwQuotaRepo
    {
        Task<List<VwQuota>> GetList(Specification<VwQuota> specification, CancellationToken cancellationToken = default);
    }
}
