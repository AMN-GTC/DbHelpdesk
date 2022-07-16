using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class ProjectSpesification : Specification<Project>
    {
        public ProjectSpesification()
        {

        }
        public Nullable<int> Idequal { get; set; }

        public Specification<Project> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }

            return this;
        }
    }
}
