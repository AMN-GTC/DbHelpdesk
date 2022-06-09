using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public abstract class Service<T> : IService where T : class
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> GetError()
        {
            return _errors;
        }

        public bool GetServiceState()
        {
            return _errors.Count == 0;
        }
        protected void ClearError()
        {
            _errors.Clear();
        }
        protected void AddError(string key, string error)
        {
            if (_errors.ContainsKey(key) == false)
            {
                _errors.Add(key, new List<string>());
            }
            _errors[key].Add(error);
        }
        protected void AddError(string error)
        {
            AddError("global error", error);
        }
    }
    public class VwActiveTicketSummaryServ : Service<VwActiveTicketSummary>, IVwActiveTicketSummaryServ
    {
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        public VwActiveTicketSummaryServ(IHelpdeskUnitOfWork helpdeskUnitOfWork)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
        }

        public Task<List<VwActiveTicketSummary>> GetList(Specification<VwActiveTicketSummary> specification, CancellationToken cancellation = default)
        {
            return _helpdeskUnitOfWork.vwActiveTicketSummaryRepo.GetList(specification, cancellation);
        }
    }
}
