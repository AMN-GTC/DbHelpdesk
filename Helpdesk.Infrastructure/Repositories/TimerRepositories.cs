using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Repositories
{
    public class TimerRepositories : ITimerRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<TimerEntity> _dbsettimer;

        public TimerRepositories(DbContext context)
        {
            _dbContext = context;
            _dbsettimer = _dbContext.Set<TimerEntity>();
        }

        public async Task Insert(TimerEntity model, CancellationToken cancellationToken = default)
        {
            await _dbsettimer.AddAsync(model, cancellationToken);
        }

        public async Task Update(TimerEntity model, int id, CancellationToken cancellationToken = default)
        {
            TimerEntity existing = await _dbsettimer.FindAsync(new object[] { id }, cancellationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Entry(existing).CurrentValues.SetValues(model);
        }

        public Task<List<TimerEntity>> GetList(Specification<TimerEntity> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<TimerEntity> query = _dbsettimer.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);
        }

        public async Task<TimerEntity> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbsettimer.FirstOrDefaultAsync(f => f.Id == id, cancelationToken);
        }
    }
}
