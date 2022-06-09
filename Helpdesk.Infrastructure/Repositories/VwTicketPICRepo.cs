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
    public class VwTicketPICRepo : IVwTicketPICRepo
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwTicketPIC> _dbpic;
        public VwTicketPICRepo(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbpic = _dbContext.Set<VwTicketPIC>();
        }
        public Task<List<VwTicketPIC>> GetList(Specification<VwTicketPIC> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwTicketPIC> query = _dbpic.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }
    }
}
