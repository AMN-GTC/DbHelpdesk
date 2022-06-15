using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Core.Specifications
{
    public class ConversationSpecification : Specification<Conversation>
    {
        public Nullable<int> Idequal { get; set; }
        public Specification<Conversation> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }
            return this;
        }
    }
}
