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
    public class VwLastWeekTicketRepo : IVwLastWeekTicketRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwLastWeekTicket> _dbVw;

        public VwLastWeekTicketRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbVw = _dbContext.Set<VwLastWeekTicket>();
        }
        public Task<List<VwLastWeekTicket>> GetList(Specification<VwLastWeekTicket> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwLastWeekTicket> query = _dbVw.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }
    }
}
