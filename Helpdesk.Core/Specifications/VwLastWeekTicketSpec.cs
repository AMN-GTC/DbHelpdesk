using Ardalis.Specification;
using Helpdesk.Core.Entities;

namespace Helpdesk.Core.Specifications
{
    public class VwLastWeekTicketSpec : Specification<VwLastWeekTicket>
    {
        public VwLastWeekTicketSpec()
        {

        }
        public double? Values { get; set; }
        public Specification<VwLastWeekTicket> Build()
        {
            if (Values.HasValue)
            {
                Query.Where(f => f.Value == Values.Value);
            }
            return this;
        }
    }
}
