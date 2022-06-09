using Ardalis.Specification;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class TimerService : Service<TimerEntity>, ITimerService
    {
        protected HelpdeskUnitOfWork _helpdeskUnitOfWork;
        public TimerService(HelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }


        public async Task<List<TimerEntity>> GetList(Specification<TimerEntity> specification, CancellationToken cancellationtoken)
        {
            return await _helpdeskUnitOfWork.Timer.GetList(specification, cancellationtoken);

        }

        public async Task<TimerEntity> Insert(TimerEntity model, CancellationToken cancellationtoken = default)
        {
            if (ValidateOnInsert(model) == false)
            {
                return null;
            }
            await _helpdeskUnitOfWork.Timer.Insert(model, cancellationtoken);
            await _helpdeskUnitOfWork.SaveChangesAsync();

            return model;
        }
        public async Task<TimerEntity> StartTimer(int ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _helpdeskUnitOfWork.Ticket.GetObject(ticketId, cancellationToken);
            if (ticket == null)
            {
                AddError("Ticket tidak boleh kosong");
                return null;
            }
            var Spec = new TimerSpesification();
            Spec.TicketidEqual = ticketId;
            Spec.IsEndNull = true;
            var timers = await _helpdeskUnitOfWork.Timer.GetList(Spec.Build(), cancellationToken = default);

            var timer = new TimerEntity();

            if (timers.Count != 0)
            {
                AddError("Timers tidak bisa di start");
                return null;
            }
            timer.start = DateTime.Now;
            timer.TicketId = ticketId;
            timer.end = null;

            await _helpdeskUnitOfWork.Timer.Insert(timer, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();
            return timer;

        }
        public async Task<TimerEntity> StopTimer(int ticketId, CancellationToken cancellationToken = default)
        {
            var ticket = await _helpdeskUnitOfWork.Ticket.GetObject(ticketId, cancellationToken);
            if (ticket == null)
            {
                AddError("Ticket tidak boleh kosong");
                return null;
            }
            var Spec = new TimerSpesification();
            Spec.TicketidEqual = ticketId;
            Spec.IsEndNull = true;
            var timers = await _helpdeskUnitOfWork.Timer.GetList(Spec.Build(), cancellationToken = default);

            if (timers == null || timers.Count == 0)
            {
                AddError("Ticket Harus di Start terlebih dahulu");
                return null;
            }

            var timer = timers[0];
            timer.end = DateTime.Now;
            await _helpdeskUnitOfWork.Timer.Update(timer, timer.Id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();

            return timer;
        }

        public async Task<TimerEntity> Update(TimerEntity model, int id, CancellationToken cancellationtoken = default)
        {
            if (ValidateOnUpdate(model) == false)
            {
                return null;
            }
            await _helpdeskUnitOfWork.Timer.Update(model, id, cancellationtoken);
            await _helpdeskUnitOfWork.SaveChangesAsync();
            return model;
        }

        protected bool ValidateOnInsert(TimerEntity timerEntity)
        {

            if (ValidateBase(timerEntity) == false)
            {
                return GetServiceState();
            }
            return GetServiceState();
        }
        protected bool ValidateOnUpdate(TimerEntity timerEntity)
        {

            if (ValidateBase(timerEntity) == false)
            {
                return GetServiceState();
            }
            return GetServiceState();


        }
        public bool ValidateBase(TimerEntity timerEntity)
        {

            var tickets = _helpdeskUnitOfWork.Ticket.GetObject(timerEntity.TicketId).Result;
            if (tickets == null)
            {
                AddError("Ticket tidak boleh kosong");
            }
            if (timerEntity.start != null && timerEntity.end != null)
            {
                if (timerEntity.start.Value > timerEntity.end.Value)
                {
                    AddError("Format waktu salah");
                }
            }

            if (timerEntity.start == null)
            {
                AddError("Start tidak boleh kosong");
            }
            if (timerEntity.end == null)
            {
                AddError("End tidak boleh kosong");
            }

            return GetServiceState();
        }

        public Task<TimerEntity> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Timer.GetObject(id, cancellationToken);
        }
    }
}
