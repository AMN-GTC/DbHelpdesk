using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class QuotaCalculationService : Service<QuotaCalculation>, IQuotaCalculationService
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;


        public QuotaCalculationService(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Quota.Delete(id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }

        public async Task<List<QuotaCalculation>> GetList(Specification<QuotaCalculation> specification, CancellationToken cancellationToken = default)
        {

            return await _helpdeskUnitOfWork.Quota.GetList(specification, cancellationToken);
        }

        public Task<QuotaCalculation> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Quota.GetObject(id, cancellationToken);
        }

        public async Task<QuotaCalculation> Insert(QuotaCalculation model, CancellationToken CancellationToken = default)
        {
            await _helpdeskUnitOfWork.Quota.Insert(model, CancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();

            return model;
        }

        public async Task<QuotaCalculation> Update(QuotaCalculation model, int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Quota.Update(model, id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return model;
        }
    }
}
