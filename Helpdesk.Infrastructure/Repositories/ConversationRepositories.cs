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
    public class ConversationRepositories : IConversationRepositories
    {
        private readonly DbContext _conversationContext;
        protected readonly DbSet<Conversation> _dbSetConversation;
        public ConversationRepositories(DbContext conversationContext)
        {
            _conversationContext = conversationContext;
            _dbSetConversation = _conversationContext.Set<Conversation>();
        }

        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancelationToken = default)
        {
            IQueryable<Conversation> query = _dbSetConversation.AsQueryable();
            SpecificationEvaluator evaluator = new SpecificationEvaluator();
            query = evaluator.GetQuery(query, specification);
            return query.ToListAsync(cancelationToken);
        }

        public async Task<Conversation> GetObject(int id, CancellationToken cancelationToken = default)
        {
            return await _dbSetConversation.FirstOrDefaultAsync(f => f.Id == id, cancelationToken);
        }

        public async Task Insert(Conversation model, CancellationToken cancelationToken = default)
        {
            await _dbSetConversation.AddAsync(model, cancelationToken);
        }
        public async Task Delete(int id, CancellationToken cancelationToken = default)
        {
            Conversation existing = await _dbSetConversation.FindAsync(new object[] { id }, cancelationToken);
            if (existing == null)
            {
                throw new Exception($"email dengan id = {id} tidak ditemukan");
            }
            _conversationContext.Remove(existing);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _conversationContext.SaveChangesAsync(cancellationToken);
        }

       
    }
}
