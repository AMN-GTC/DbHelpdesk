using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class TicketService : Service<Ticket>, ITicketService
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public TicketService(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }
        public async Task<Ticket> Insert(Ticket ticket, CancellationToken cancellationToken = default)
        {
            if (ValidateInsert(ticket) == false)
            {
                return null;
            }
            await _helpdeskUnitOfWork.Ticket.Insert(ticket, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return ticket;
        }
        protected bool ValidateInsert(Ticket ticket)
        {
            if (string.IsNullOrEmpty(ticket.Title))
            {
                AddError("title harus diisi");
            }
            return GetServiceState();
        }
        public async Task<Ticket> Update(Ticket ticket, int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Ticket.Update(ticket, id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return ticket;
        }
        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Ticket.Delete(id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<List<Ticket>> GetList(Specification<Ticket> specification, CancellationToken cancellationToken = default)
        {
            var tiketList = await _helpdeskUnitOfWork.Ticket.GetList(specification, cancellationToken);
            foreach (var item in tiketList)
            {
                var project = await _helpdeskUnitOfWork.Project.GetObject(item.ProjectId, cancellationToken);
                item.Application = project.Name;
                item.Project = project;

                var user = await _helpdeskUnitOfWork.User.GetObject(item.UserId, cancellationToken);
                item.User = user;
                item.Assign_to_username = user.Name;

                var status = await _helpdeskUnitOfWork.Status.GetObject(item.StatusId, cancellationToken);
                item.TicketStatus = status;
                item.Ticket_status = status.Name;

                var spec = new TimerSpesification()
                {
                    TicketidEqual = item.Id
                };
                var timers = await _helpdeskUnitOfWork.Timer.GetList(spec.Build());
            }
            return tiketList;
        }

        public Task<Ticket> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Ticket.GetObject(id, cancellationToken);
        }

    }
}
