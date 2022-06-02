using Helpdesk.Core.Entities;
using Helpdesk.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using System;

namespace Helpdesk.Infrastructure
{
    public class HelpdeskDbContext : DbContext
    {
        public HelpdeskDbContext(DbContextOptions<HelpdeskDbContext> options) : base(options) { }
            public DbSet<VwTicketSummary> VwTicketSummaries { get; set; }
            public DbSet<VwTicketPIC> VwTicketPIC { get; set; }
            public DbSet<VwActiveTicketSummary> VwActiveTicketSummaries { get; set; }
            public DbSet<Ticket> TicketSet { get; set; }
            public DbSet<TimerEntity> TimerSet { get; set; }
            public DbSet<User> UserSet { get; set; }
            public DbSet<Project> ProjectSet { get; set; }
            public DbSet<Status> StatusSet { get; set; }
            public DbSet<VwExcelReportDetail> VwExcelReportDetails { get; set; }
            
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder
                    .Entity<VwTicketPIC>()
                    .ToView(nameof(VwTicketPIC))
                    .HasNoKey();

                base.OnModelCreating(modelBuilder);
                modelBuilder
                    .Entity<VwTicketSummary>()
                    .ToView(nameof(VwTicketSummaries))
                    .HasNoKey();

                base.OnModelCreating(modelBuilder);
                modelBuilder
                    .Entity<VwActiveTicketSummary>()
                    .ToView(nameof(VwActiveTicketSummaries))
                    .HasNoKey();

                base.OnModelCreating(modelBuilder);
                modelBuilder.ApplyConfiguration<Ticket>(new TicketConfiguration());
                modelBuilder.ApplyConfiguration<TimerEntity>(new TimerConfiguration());
                modelBuilder.ApplyConfiguration<User>(new UserConfiguration());
                modelBuilder.ApplyConfiguration<Project>(new ProjectConfiguration());
                modelBuilder.ApplyConfiguration<Status>(new StatusConfiguration());
                modelBuilder.ApplyConfiguration<VwExcelReportDetail>(new VwExcelReportDetailConfiguration());
        }
    }
}
