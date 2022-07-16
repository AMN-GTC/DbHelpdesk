using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IVwExcelReportTicketDetailAllRepositories
    {
        Task<VwExcelReportTicketDetailAll> GetObject(int year, CancellationToken cancelationToken = default);
        Task<List<VwExcelReportTicketDetailAll>> GetList(Specification<VwExcelReportTicketDetailAll> specification, CancellationToken cancellationToken = default);
    }
}
