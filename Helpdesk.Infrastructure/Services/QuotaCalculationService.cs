using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class QuotaCalculationService : IQuotaCalculationService
    {
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Dictionary<string, List<string>> GetError()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<QuotaCalculation>> GetList(Specification<QuotaCalculation> specification, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<QuotaCalculation> GetObject(int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public bool GetServiceState()
        {
            throw new System.NotImplementedException();
        }

        public Task<QuotaCalculation> Insert(QuotaCalculation Status, CancellationToken CancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<QuotaCalculation> Update(QuotaCalculation Status, int id, CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
