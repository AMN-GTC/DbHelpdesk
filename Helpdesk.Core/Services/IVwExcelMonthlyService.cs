using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IVwExcelMonthlyService : IService
    {
        public Task<VwExcelMonthly> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<VwExcelMonthly>> GetList(Specification<VwExcelMonthly> specification, CancellationToken cancellationToken = default);

    }
}
