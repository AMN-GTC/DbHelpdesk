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
    public class VwExcelReportTicketDetailAllRepositories : IVwExcelReportTicketDetailAllRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<VwExcelReportTicketDetailAll> _dbsetreportall;

        public VwExcelReportTicketDetailAllRepositories(DbContext context)
        {
            _dbContext = context;
            _dbsetreportall = _dbContext.Set<VwExcelReportTicketDetailAll>();
        }

        public Task<List<VwExcelReportTicketDetailAll>> GetList(Specification<VwExcelReportTicketDetailAll> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<VwExcelReportTicketDetailAll> query = _dbsetreportall.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }

        public async Task<VwExcelReportTicketDetailAll> GetObject(int year, CancellationToken cancelationToken = default)
        {
            return await _dbsetreportall.FirstOrDefaultAsync(cancelationToken);
        }
    }
}
