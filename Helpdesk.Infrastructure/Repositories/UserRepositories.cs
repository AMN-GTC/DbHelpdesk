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
    public class UserRepositories : IUserRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<User> _dbsetuser;

        public UserRepositories(DbContext context)
        {
            _dbContext = context;
            _dbsetuser = _dbContext.Set<User>();
        }
        public async Task Delete(int id, CancellationToken cancelationToken = default)
        {
            User existing = await _dbsetuser.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Remove(existing);
        }

        public Task<List<User>> GetList(Specification<User> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<User> query = _dbsetuser.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<User> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbsetuser.FirstOrDefaultAsync(x => x.Id == id, cancelationToken);
        }

        public async Task Insert(User model, CancellationToken cancelationToken = default)
        {
            await _dbsetuser.AddAsync(model, cancelationToken);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task Update(User model, int id, CancellationToken cancelationToken = default)
        {
            User existing = await _dbsetuser.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model dengan id ={id} tidak ditemukan");
            }

            _dbContext.Entry(existing).CurrentValues.SetValues(model);
        }
    }
}
