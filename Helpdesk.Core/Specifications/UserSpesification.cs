
using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class UserSpesification : Specification<User>
    {
        public UserSpesification()
        {

        }
        public Nullable<int> Idequal { get; set; }
        public Specification<User> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }
            return this;
        }
    }
}
