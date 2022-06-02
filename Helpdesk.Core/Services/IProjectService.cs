using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface IProjectService : IService
    {
        public Task<Project> Insert(Project Project, CancellationToken CancellationToken = default);
        public Task<Project> Update(Project Project, int id, CancellationToken cancellationToken = default);
        public Task<bool> Delete(int id, CancellationToken cancellationToken = default);
        public Task<Project> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<Project>> GetList(Specification<Project> specification, CancellationToken cancellationToken = default);
    }
}
