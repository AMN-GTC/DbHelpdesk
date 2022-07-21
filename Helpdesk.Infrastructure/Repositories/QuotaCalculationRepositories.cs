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
    public class QuotaCalculationRepositories : IQuotaCalculationRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<QuotaCalculation> _dbsetquota;

        public QuotaCalculationRepositories(DbContext context)
        {
            _dbContext = context;
            _dbsetquota = _dbContext.Set<QuotaCalculation>();
        }

        public async Task Delete(int id, CancellationToken cancelationToken = default)
        {
            QuotaCalculation existing = await _dbsetquota.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Remove(existing);
        }

        public async Task Insert(QuotaCalculation model, CancellationToken cancelationToken = default)
        {
            await _dbsetquota.AddAsync(model, cancelationToken);
        }

        public async Task Update(QuotaCalculation model, int id, CancellationToken cancelationToken = default)
        {
            QuotaCalculation existing = await _dbsetquota.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model dengan id ={id} tidak ditemukan");
            }

            _dbContext.Entry(existing).CurrentValues.SetValues(model);
        }

        public Task<List<QuotaCalculation>> GetList(Specification<QuotaCalculation> specification, CancellationToken cancelationToken)
        {
            IQueryable<QuotaCalculation> query = _dbsetquota.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<QuotaCalculation> GetObject(int id, CancellationToken cancelationToken)
        {
            return await _dbsetquota.FirstOrDefaultAsync(x => x.Id == id, cancelationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
