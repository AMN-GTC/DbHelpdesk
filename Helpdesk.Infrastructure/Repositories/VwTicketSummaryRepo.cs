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
    public class VwTicketSummaryRepo : IVwTicketSummaryRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwTicketSummary> _dbVw;

        public VwTicketSummaryRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbVw = _dbContext.Set<VwTicketSummary>();
        }
        public Task<List<VwTicketSummary>> GetList(Specification<VwTicketSummary> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwTicketSummary> query = _dbVw.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }
    }
}
