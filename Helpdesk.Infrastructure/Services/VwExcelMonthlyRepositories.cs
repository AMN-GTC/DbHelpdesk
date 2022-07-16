using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class VwExcelMonthlyService : IVwExcelMonthlyService
    {
        public Dictionary<string, List<string>> GetError()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<VwExcelMonthly>> GetList(Specification<VwExcelMonthly> specification, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<VwExcelMonthly> GetObject(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public bool GetServiceState()
        {
            throw new System.NotImplementedException();
        }
    }
}
