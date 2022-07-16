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
    public class VwExcelMonthlyRepositories : IVwExcelMonthlyRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwExcelMonthly> _dbSetExcelMothly;

        public VwExcelMonthlyRepositories(DbContext context)
        {
            _dbContext = context;
            _dbSetExcelMothly = _dbContext.Set<VwExcelMonthly>();
        }
        public Task<List<VwExcelMonthly>> GetList(Specification<VwExcelMonthly> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<VwExcelMonthly> query = _dbSetExcelMothly.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<VwExcelMonthly> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbSetExcelMothly.FirstOrDefaultAsync(f => f.Id == id, cancelationToken);
        }

    }
}
