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
        private IVwTicketSummaryRepo _vwTicketSummary;
        private IVwTicketPICRepo _vwPIC;
        private IVwActiveTicketSummaryRepo _vwats;
        private ITicketRepositories _tickets;
        private ITimerRepositories _timer;
        private IProjectRepositories _project;
        private IUserRepositories _user;
        private IStatusRepositories _status;
        private IVwExcelReportDetailRepository _excelReportDetailRepository;
        public ITicketRepositories Ticket => _tickets = _tickets ?? new TicketRepositories(_dbContext);

        public ITimerRepositories Timer => _timer = _timer ?? new TimerRepositories(_dbContext);

        public IProjectRepositories Project => _project = _project ?? new ProjectRepositories(_dbContext);

        public IUserRepositories User => _user = _user ?? new UserRepositories(_dbContext);

        public IStatusRepositories Status => _status = _status ?? new StatusRepositories(_dbContext);

        public IVwExcelReportDetailRepository ExcelReportDetail => _excelReportDetailRepository = _excelReportDetailRepository ?? new VwExcelReportDetailRepository(_dbContext);

        public IVwTicketSummaryRepo vwTicketSummaryrepo => _vwTicketSummary = _vwTicketSummary ?? new VwTicketSummaryRepo(_dbContext);

        public IVwTicketPICRepo vwTicketPICrepo => _vwPIC = _vwPIC ?? new VwTicketPICRepo(_dbContext);

        public IVwActiveTicketSummaryRepo vwActiveTicketSummaryRepo => _vwats = _vwats ?? new VwActiveTicketSumarryRepo(_dbContext);
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
