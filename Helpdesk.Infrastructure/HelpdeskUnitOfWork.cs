using Helpdesk.Core;
using Helpdesk.Core.Repositories;
using Helpdesk.Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure
{
    public class HelpdeskUnitOfWork : IHelpdeskUnitOfWork
    {
        public HelpdeskDbContext _dbContext;
        public HelpdeskUnitOfWork(HelpdeskDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        private ITicketRepositories _tickets;
        private ITimerRepositories _timer;
        private IProjectRepositories _project;
        private IUserRepositories _user;
        private IStatusRepositories _status;
        private IVwExcelReportDetailRepository _excelReportDetailRepository;
        private IVwExcelReportTicketDetailAllRepositories _excelReportTicketDetailAll;
        private IQuotaCalculationRepositories _quotaCalculation;
        private IVwExcelMonthlyRepositories _excelMonthly;
        private IVwTicketSummaryRepo _vwTicketSummary;
        private IVwTicketPICRepo _vwPIC;
        private IVwActiveTicketSummaryRepo _vwats;
        private IVwLastWeekTicketRepo _vwLast;
        private IVwQuotaRepo _vwQuota;
        private IEmailRepository _emailRepository;
        private IEmailStackRepository _emailStackRepository;
        private IConversationRepositories _conversations;

        public ITicketRepositories Ticket => _tickets = _tickets ?? new TicketRepositories(_dbContext);

        public ITimerRepositories Timer => _timer = _timer ?? new TimerRepositories(_dbContext);

        public IProjectRepositories Project => _project = _project ?? new ProjectRepositories(_dbContext);

        public IUserRepositories User => _user = _user ?? new UserRepositories(_dbContext);

        public IStatusRepositories Status => _status = _status ?? new StatusRepositories(_dbContext);

        public IVwExcelReportDetailRepository ExcelReportDetail => _excelReportDetailRepository = _excelReportDetailRepository ?? new VwExcelReportDetailRepository(_dbContext);

        public IVwTicketSummaryRepo vwTicketSummaryrepo => _vwTicketSummary = _vwTicketSummary ?? new VwTicketSummaryRepo(_dbContext);

        public IVwTicketPICRepo vwTicketPICrepo => _vwPIC = _vwPIC ?? new VwTicketPICRepo(_dbContext);

        public IVwActiveTicketSummaryRepo vwActiveTicketSummaryRepo => _vwats = _vwats ?? new VwActiveTicketSumarryRepo(_dbContext);

        public IVwLastWeekTicketRepo vwLastWeekRepo => _vwLast = _vwLast ?? new VwLastWeekTicketRepo(_dbContext);

        public IVwQuotaRepo vwQuotaRepo => _vwQuota = _vwQuota ?? new VwQuotaRepo(_dbContext);
        public IVwExcelReportDetailRepository VwExcelReportDetail => _excelReportDetailRepository = _excelReportDetailRepository ?? new VwExcelReportDetailRepository(_dbContext);

        public IVwExcelReportTicketDetailAllRepositories VwExcelReportTicketDetailAll => _excelReportTicketDetailAll = _excelReportTicketDetailAll ?? new VwExcelReportTicketDetailAllRepositories(_dbContext);

        public IQuotaCalculationRepositories Quota => _quotaCalculation = _quotaCalculation ?? new QuotaCalculationRepositories(_dbContext);

        public IVwExcelMonthlyRepositories VwExcelMonthly => _excelMonthly = _excelMonthly ?? new VwExcelMonthlyRepositories(_dbContext);
        public IConversationRepositories Conversation => _conversations = _conversations ?? new ConversationRepositories(_dbContext);
        public IEmailRepository email => _emailRepository = _emailRepository ?? new EmailRepository(_dbContext);

        public IEmailStackRepository emailStack => _emailStackRepository = _emailStackRepository ?? new EmailStackRepository(_dbContext);

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
