using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class VwActiveTicketSummarySpec : Specification<VwActiveTicketSummary>
    {
        public VwActiveTicketSummarySpec()
        {

        }
        public double? Latess { get; set; }
        public double? ClosedTickets { get; set; }
        public double? OpenTickets { get; set; }
        public double? InProggresss { get; set; }
        public string Projects { get; set; }

        public Specification<VwActiveTicketSummary> Build()
        {
            if (!string.IsNullOrEmpty(Projects))
            {
                Query.Where(f => f.Project != null);
            }
            if (Latess.HasValue)
            {
                Query.Where(f => f.Late == Latess.Value);
            }
            if (ClosedTickets.HasValue)
            {
                Query.Where(f => f.ClosedTicket == ClosedTickets);
            }
            if (OpenTickets.HasValue)
            {
                Query.Where(f => f.OpenTicket == OpenTickets);
            }
            if (InProggresss.HasValue)
            {
                Query.Where(f => f.InProgress == InProggresss);
            }
            return this;
        }
    }
}