using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Helpdesk.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class ProjectService : Service<Project>, IProjectService
    {
        protected HelpdeskUnitOfWork _helpdeskUnitOfWork;
        public ProjectService(HelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Project.Delete(id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<Project>> GetList(Specification<Project> specification, CancellationToken cancellationToken = default)
        {
            var projectList = await _helpdeskUnitOfWork.Project.GetList(specification, cancellationToken);
            foreach (var item in projectList)
            {
                var spec = new TicketSpecification()
                {
                    ProjectIdequal = item.Id
                };
                var ticket = await _helpdeskUnitOfWork.Ticket.GetList(spec.Build());


            }
            return projectList;
            //return _helpdeskUnitOfWork.Project.GetList(specification, cancellationToken);
        }

        public Task<Project> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Project.GetObject(id, cancellationToken);

        }

        public async Task<Project> Insert(Project Project, CancellationToken CancellationToken = default)
        {
            await _helpdeskUnitOfWork.Project.Insert(Project, CancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(CancellationToken);
            return Project;
        }

        public async Task<Project> Update(Project Project, int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Project.Update(Project, id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return Project;
        }
    }
}
