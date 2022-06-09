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
    public class TicketRepositories : ITicketRepositories
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<Ticket> _dbSetTicket;

        public TicketRepositories(DbContext context)
        {
            _dbContext = context;
            _dbSetTicket = _dbContext.Set<Ticket>();
        }
        public async Task Delete(int id, CancellationToken cancelationToken = default)
        {
            Ticket existing = await _dbSetTicket.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Remove(existing);
        }

        public Task<List<Ticket>> GetList(Specification<Ticket> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<Ticket> query = _dbSetTicket.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);

        }

        public async Task<Ticket> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbSetTicket.FirstOrDefaultAsync(f => f.Id == id, cancelationToken);
        }

        public async Task Insert(Ticket model, CancellationToken cancelationToken = default)
        {
            await _dbSetTicket.AddAsync(model, cancelationToken);
        }

        public async Task Update(Ticket model, int id, CancellationToken cancelationToken = default)
        {
            Ticket existing = await _dbSetTicket.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"model item dengan id = {id} tidak ditemukan");
            }
            _dbContext.Entry(existing).CurrentValues.SetValues(model);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
