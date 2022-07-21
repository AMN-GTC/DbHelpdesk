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
    public class ProjectRepositories : IProjectRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<Project> _dbsetproject;

        public ProjectRepositories(DbContext context)
        {
            _dbContext = context;
            _dbsetproject = _dbContext.Set<Project>();
        }
        public async Task Delete(int id, CancellationToken cancelationToken = default)
        {
            Project existing = await _dbsetproject.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Remove(existing);
        }

        public Task<List<Project>> GetList(Specification<Project> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<Project> query = _dbsetproject.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<Project> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbsetproject.FirstOrDefaultAsync(x => x.Id == id, cancelationToken);
        }

        public async Task Insert(Project model, CancellationToken cancelationToken = default)
        {
            await _dbsetproject.AddAsync(model, cancelationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(Project model, int id, CancellationToken cancelationToken = default)
        {
            Project existing = await _dbsetproject.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Entry(existing).CurrentValues.SetValues(model);
        }
    }
}
