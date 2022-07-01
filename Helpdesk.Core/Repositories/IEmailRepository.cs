using System;
using Ardalis.Specification;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Core.Repositories
{
    public interface IEmailRepository
    {
        Task Insert (Email model, CancellationToken cancellationToken = default);

        Task Update(Email model, string id , CancellationToken cancellationToken = default);
        
        Task Delete(string id, CancellationToken cancellationToken = default);
        
        Task<Email> GetObject(string id /*int id*/, CancellationToken cancellationToken = default);
        
        Task<List<Email>> GetList(Specification<Email> filter, CancellationToken cancellationToken = default);

    }
}
