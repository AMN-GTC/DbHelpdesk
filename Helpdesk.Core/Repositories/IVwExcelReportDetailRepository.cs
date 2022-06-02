using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwExcelReportDetailRepository
    {
        Task Insert(VwExcelReportDetail model, CancellationToken cancellationToken = default);
        Task<VwExcelReportDetail> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<VwExcelReportDetail>> GetList(Specification<VwExcelReportDetail> specification, CancellationToken cancellationToken = default);
    }
}
