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
    public class StatusRepositories : IStatusRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<Status> _dbSetStatus;

        public StatusRepositories(DbContext context)
        {
            _dbContext = context;
            _dbSetStatus = _dbContext.Set<Status>();
        }
        public Task Delete(int id, CancellationToken cancelationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Status>> GetList(Specification<Status> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<Status> query = _dbSetStatus.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<Status> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbSetStatus.FirstOrDefaultAsync(f => f.Id == id, cancelationToken);
        }

        public Task Insert(Status model, CancellationToken cancelationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Status model, int id, CancellationToken cancelationToken = default)
        {
            throw new System.NotImplementedException();
        }
    }
}
