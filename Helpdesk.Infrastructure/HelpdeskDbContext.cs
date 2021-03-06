using Helpdesk.Core.Entities;
using Helpdesk.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Helpdesk.Infrastructure
{
    public class HelpdeskDbContext : DbContext
    {
        public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) { }
        public DbSet<Ticket> TicketSet { get; set; }
        public DbSet<TimerEntity> TimerSet { get; set; }
        public DbSet<User> UserSet { get; set; }
        public DbSet<Project> ProjectSet { get; set; }
        public DbSet<Status> StatusSet { get; set; }
        public DbSet<VwExcelReportDetail> VwExcelReportDetails { get; set; }
        public DbSet<VwExcelReportTicketDetailAll> VwExcelReportTicketDetailAll { get; set; }
        public DbSet<QuotaCalculation> QuotaSet { get; set; }
        public DbSet<VwExcelMonthly> VwExcelMonthly { get; set; }
        public DbSet<VwTicketSummary> VwSumChartTicketSumm { get; set; }
        public DbSet<VwLastWeekTicket> VwLastWeek { get; set; }
        public DbSet<VwTicketPIC> VwTicketPIC { get; set; }
        public DbSet<VwActiveTicketSummary> VwSumChartActiveTicketSummaries { get; set; }
        public DbSet<VwQuota> VwQuotaSummary { get; set; }
        public DbSet<Conversation> ConversationSet { get; set; }
        public DbSet<EmailStack> EmailStack { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VwQuota>()
                .ToView(nameof(VwQuotaSummary))
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VwLastWeekTicket>()
                .ToView(nameof(VwLastWeek))
                .HasNoKey();
            
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VwTicketPIC>()
                .ToView(nameof(VwTicketPIC))
                .HasNoKey();
            
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VwTicketSummary>()
                .ToView(nameof(VwSumChartTicketSumm))
                .HasNoKey();
            
            base.OnModelCreating(modelBuilder);
            modelBuilder
                .Entity<VwActiveTicketSummary>()
                .ToView(nameof(VwSumChartActiveTicketSummaries))
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Ticket>(new TicketConfiguration());
            modelBuilder.ApplyConfiguration<TimerEntity>(new TimerConfiguration());
            modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
            modelBuilder.ApplyConfiguration<Project>(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration<Status>(new StatusConfiguration());
            modelBuilder.ApplyConfiguration<VwExcelReportDetail>(new VwExcelReportDetailConfiguration());
            modelBuilder.ApplyConfiguration<VwExcelReportTicketDetailAll>(new VwExcelReportTicketDetailAllConfiguration());
            modelBuilder.ApplyConfiguration<QuotaCalculation>(new QuotaCalculationConfiguration());
            modelBuilder.ApplyConfiguration<VwExcelMonthly>(new VwExcelMonthlyConfiguration());
            modelBuilder.ApplyConfiguration<Conversation>(new ConversationConfiguration());
            modelBuilder.ApplyConfiguration<EmailStack>(new EmailStackConfiguration());
        }
    }
}
