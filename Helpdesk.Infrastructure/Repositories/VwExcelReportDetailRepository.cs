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
    public class VwExcelReportDetailRepository : IVwExcelReportDetailRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwExcelReportDetail> _dbsetreport;

        public VwExcelReportDetailRepository(DbContext context)
        {
            _dbContext = context;
            _dbsetreport = _dbContext.Set<VwExcelReportDetail>();
        }
        public Task<List<VwExcelReportDetail>> GetList(Specification<VwExcelReportDetail> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwExcelReportDetail> query = _dbsetreport.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }

        public async Task<VwExcelReportDetail> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbsetreport.FirstOrDefaultAsync(x => x.Id == id, cancelationToken);
        }

        public async Task Insert(VwExcelReportDetail model, CancellationToken cancellationToken = default)
        {
            await _dbsetreport.AddAsync(model, cancellationToken);
        }
    }
}
