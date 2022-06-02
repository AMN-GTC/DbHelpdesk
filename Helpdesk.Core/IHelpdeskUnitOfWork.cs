using Helpdesk.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Core
{
        public interface IHelpdeskUnitOfWork
        {
        IVwTicketSummaryRepo vwTicketSummaryrepo { get; }
        IVwTicketPICRepo vwTicketPICrepo { get; }
        IVwActiveTicketSummaryRepo vwActiveTicketSummaryRepo { get; }
        ITicketRepositories Ticket { get; }
        ITimerRepositories Timer { get; }
        IProjectRepositories Project { get; }
        IUserRepositories User { get; }
        IStatusRepositories Status { get; }
        IVwExcelReportDetailRepository ExcelReportDetail { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
