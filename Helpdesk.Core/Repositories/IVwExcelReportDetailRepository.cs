using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwExcelReportDetailRepository
    {
        Task<VwExcelReportDetail> GetObject(int year, int month, CancellationToken cancelationToken = default);
        Task<List<VwExcelReportDetail>> GetList(Specification<VwExcelReportDetail> specification, CancellationToken cancellationToken = default);
    }
}
