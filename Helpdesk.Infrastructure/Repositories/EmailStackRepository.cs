using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Helpdesk.Infrastructure.Repositories
{
    public class EmailStackRepository : IEmailStackRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<EmailStack> _dbsetEmailStack;

        public EmailStackRepository(DbContext context)
        {
            _dbContext = context;
            _dbsetEmailStack = _dbContext.Set<EmailStack>();
        }


        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            EmailStack existingStack = await _dbsetEmailStack.FindAsync(new object [] {id},cancellationToken);
            if (existingStack == null)
            {
                throw new Exception($"Data dengan Id {id} tidak dapat ditemukan!");
            }
            _dbsetEmailStack.Remove(existingStack);
        }


        public Task<List<EmailStack>> GetList(Specification<EmailStack> specification, CancellationToken cancellationToken = default)
        {
            IQueryable<EmailStack> query = _dbsetEmailStack.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancellationToken);

        }


        public async Task<EmailStack> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return await _dbsetEmailStack.FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
        }


        public async Task Insert(EmailStack model, CancellationToken cancellationToken = default)
        {
            await _dbsetEmailStack.AddAsync(model, cancellationToken);
        }

        public async Task Update(EmailStack model, int id, CancellationToken cancellationToken = default)
        {
            EmailStack existingStack = await _dbsetEmailStack.FindAsync(new object [] { id}, cancellationToken );
            if (existingStack == null)
            {
                throw new Exception($"Data dengan Id {id} tidak dapat ditemukan!");
            }
            _dbContext.Entry(existingStack).CurrentValues.SetValues(model);

        }
    }
}
