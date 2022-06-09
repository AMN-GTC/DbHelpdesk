﻿using API_DB_Conversation.Entity;
using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API_DB_Conversation.Services
{
    public interface IService
    {
        Dictionary<string, List<string>> GetError();
        Boolean GetServiceState();
    }
    public interface IConversationService : IService
    {
        public Task SendEmailAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public Task<Conversation> Insert(Conversation conversation, CancellationToken CancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Conversation> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancellationToken = default);
        //public Task<bool> GetConversationFromEmail(CancellationToken cancellationToken = default);
        //public Task<IReadOnlyList<Conversation>> GetConversations(ISpecification<Conversation> filter, CancellationToken cancellationToken = default);
    }
}