using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core.Repositories
{
    public interface IEmailStackRepository
    {
        Task Insert(EmailStack model, CancellationToken cancellationToken = default);

        Task Update(EmailStack model, int id, CancellationToken cancellationToken = default);

        Task Delete(int id, CancellationToken cancellationToken = default);

        Task<EmailStack> GetObject(int id, CancellationToken cancellationToken = default);

        Task<List<EmailStack>> GetList(Specification<EmailStack> specification, CancellationToken cancellationToken = default);
    }
}
