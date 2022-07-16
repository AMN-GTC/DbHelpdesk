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

        ITicketRepositories Ticket { get; }
        ITimerRepositories Timer { get; }
        IProjectRepositories Project { get; }
        IUserRepositories User { get; }
        IStatusRepositories Status { get; }
        IQuotaCalculationRepositories Quota { get; }
        IVwExcelReportDetailRepository VwExcelReportDetail { get; }
        IVwExcelReportTicketDetailAllRepositories VwExcelReportTicketDetailAll { get; }
        IVwExcelMonthlyRepositories VwExcelMonthly { get; }
        IVwQuotaRepo vwQuotaRepo { get; }
        IVwTicketSummaryRepo vwTicketSummaryrepo { get; }
        IVwTicketPICRepo vwTicketPICrepo { get; }
        IVwActiveTicketSummaryRepo vwActiveTicketSummaryRepo { get; }
        IVwLastWeekTicketRepo vwLastWeekRepo { get; }
        IConversationRepositories Conversation { get; }
        IEmailStackRepository emailStack { get; }
        IEmailRepository email { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

    }
}
