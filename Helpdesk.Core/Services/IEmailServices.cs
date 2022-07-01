using System;
using System.Threading;
using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Core.Services
{
    public interface IServiceEmail
    {
        Dictionary<string, List<string>> GetError();

        Boolean GetServiceState();
    }
    public interface IEmailServices : IServiceEmail
    {
        Task<Email> Insert(Email emailStack, CancellationToken cancellationToken = default);
        Task<bool> Update(Email emailStack, string id, CancellationToken cancellationToken = default);
        Task<bool> Delete(string id, CancellationToken cancellationToken);
        Task<List<Email>> GetList(Specification<Email> specification, CancellationToken cancellationToken = default);
        Task<Email> GetObject(Email emailStack, string id, CancellationToken cancellationToken = default);

    }
}
