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
    public class VwActiveTicketSumarryRepo : IVwActiveTicketSummaryRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwActiveTicketSummary> _dbvwats;
        public VwActiveTicketSumarryRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbvwats = _dbContext.Set<VwActiveTicketSummary>();
        }

        public Task<List<VwActiveTicketSummary>> GetList(Specification<VwActiveTicketSummary> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwActiveTicketSummary> query = _dbvwats.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }
    }
}
