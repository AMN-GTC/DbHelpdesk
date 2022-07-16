using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IQuotaCalculationService : IService
    {
        public Task<QuotaCalculation> Insert(QuotaCalculation Status, CancellationToken CancellationToken = default);
        public Task<QuotaCalculation> Update(QuotaCalculation Status, int id, CancellationToken cancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<QuotaCalculation> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<QuotaCalculation>> GetList(Specification<QuotaCalculation> specification, CancellationToken cancellationToken = default);
    }
}
