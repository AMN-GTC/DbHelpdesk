using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Services
{
    public interface ITimerService : IService
    {
        public Task<TimerEntity> Insert(TimerEntity model, CancellationToken cancellationtoken = default);
        public Task<TimerEntity> Update(TimerEntity model, int id, CancellationToken cancellationtoken = default);
        public Task<TimerEntity> StartTimer(int ticketId, CancellationToken cancellationtoken = default);
        public Task<TimerEntity> StopTimer(int ticketId, CancellationToken cancellationtoken = default);
        public Task<TimerEntity> GetObject(int id, CancellationToken cancellationToken = default);
        public Task<List<TimerEntity>> GetList(Specification<TimerEntity> specification, CancellationToken cancellationtoken);


    }
}
