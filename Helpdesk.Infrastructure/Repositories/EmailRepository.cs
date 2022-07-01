using Helpdesk.Core.Entities;
using Helpdesk.Core.Repositories;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Infrastructure.Repositories
{
    public class EmailRepository : IEmailRepository
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<Email> _dbSetEmail;

        public EmailRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSetEmail = _dbContext.Set<Email>();
        }
        public async Task Delete(string id, CancellationToken cancellationToken = default)
        {
            Email email = await _dbSetEmail.FindAsync(new object [] {id}, cancellationToken);
            if(email == null)
            {
                throw new Exception($"model email dengan id = {id} tidak dapat ditemukan!");
            }
            _dbContext.Remove(email);
        }

        public Task<List<Email>> GetList(Specification<Email> filter, CancellationToken cancellationToken = default)
        {
            IQueryable<Email> query  = _dbSetEmail.AsQueryable();  
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, filter);
            return query.ToListAsync(cancellationToken);
        }

        public async Task<Email> GetObject(string id, CancellationToken cancellationToken = default)
        {
            return await _dbSetEmail.FirstOrDefaultAsync(f => f.MsgID == id, cancellationToken);
        }

        public async Task Insert(Email model, CancellationToken cancellationToken = default)
        {
            await _dbSetEmail.AddAsync(model, cancellationToken);
        }

        public async Task Update(Email model,string id, CancellationToken cancellationToken = default)
        {
            Email emailStack = await _dbSetEmail.FindAsync(new object[] { id }, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
