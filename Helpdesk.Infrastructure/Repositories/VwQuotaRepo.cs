using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Repositories
{
    public class VwQuotaRepo : IVwQuotaRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwQuota> _dbvwquota;
        public VwQuotaRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbvwquota = _dbContext.Set<VwQuota>();
        }

        public Task<List<VwQuota>> GetList(Specification<VwQuota> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwQuota> query = _dbvwquota.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }
    }
}
