using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class VwTicketPICSpec : Specification<VwTicketPIC>
    {
        public VwTicketPICSpec()
        {

        }
        public double? Values { get; set; }
        public string PICs { get; set; }
        public Specification<VwTicketPIC> Build()
        {
            if (Values.HasValue)
            {
                Query.Where(f => f.Value == Values.Value);
            }
            if (!string.IsNullOrEmpty(PICs))
            {
                Query.Where(f => f.PIC != null);
            }
            return this;
        }
    }
}