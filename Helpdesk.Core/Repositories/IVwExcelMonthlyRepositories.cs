using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwExcelMonthlyRepositories
    {
        Task<VwExcelMonthly> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<VwExcelMonthly>> GetList(Specification<VwExcelMonthly> specification, CancellationToken cancelationToken = default);
    }
}
