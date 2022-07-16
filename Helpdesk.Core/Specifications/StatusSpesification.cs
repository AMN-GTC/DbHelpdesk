using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class StatusSpesification : Specification<Status>
    {
        public StatusSpesification()
        {

        }
        public Nullable<int> Idequal { get; set; }
        public string Nameequal { get; set; }

        public Specification<Status> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }
            if (!string.IsNullOrEmpty(Nameequal))
            {
                Query.Where(f => f.Name != null);
            }
            return this;
        }

    }
}
