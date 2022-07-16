using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class VwTicketSummarySpec : Specification<VwTicketSummary>
    {
        public VwTicketSummarySpec()
        {

        }
        public double? Values { get; set; }
        public string Categories { get; set; }
        public string ChartStats { get; set; }
        public Specification<VwTicketSummary> Build()
        {
            if (Values.HasValue)
            {
                Query.Where(f => f.Value == Values.Value);
            }
            /*if (!string.IsNullOrEmpty(Categories))
            {
                Query.Where(f => f.Category != null);
            }*/
            if (!string.IsNullOrEmpty(ChartStats))
            {
                Query.Where(f => f.ChartStat != null);
            }
            return this;
        }


    }
}
