using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public interface IVwQuotaServ : IService
{
    Task<List<VwQuota>> GetList(Specification<VwQuota> specification, CancellationToken cancellation = default);

}
