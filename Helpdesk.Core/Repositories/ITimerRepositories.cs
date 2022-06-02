using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface ITimerRepositories
    {

        Task Insert(TimerEntity model, CancellationToken cancellationToken = default);
        Task Update(TimerEntity model, int id, CancellationToken cancellationToken = default);
        Task<TimerEntity> GetObject(int id, CancellationToken cancelationToken = default);
        Task<List<TimerEntity>> GetList(Specification<TimerEntity> specification, CancellationToken cancellationToken = default);
    }
}
