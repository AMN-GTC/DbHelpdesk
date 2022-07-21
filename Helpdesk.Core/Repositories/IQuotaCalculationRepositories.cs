using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IQuotaCalculationRepositories
    {
        Task Insert(QuotaCalculation model, CancellationToken cancelationToken = default);
        Task Update(QuotaCalculation model, int id, CancellationToken cancelationToken = default);
        Task Delete(int id, CancellationToken cancelationToken = default);
        Task<QuotaCalculation> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<QuotaCalculation>> GetList(Specification<QuotaCalculation> specification, CancellationToken cancelationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
