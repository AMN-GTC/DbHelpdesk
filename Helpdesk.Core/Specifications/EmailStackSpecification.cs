using Helpdesk.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;

namespace Helpdesk.Core.Specifications
{
    public class EmailStackSpecification : Specification<EmailStack>
    {
        public EmailStackSpecification()
        {
            
        }


        public bool IsProcessedEqual { get; set; }
        public string SubjectContains  { get; set; }


        public Specification<EmailStack> ToSpecification()
        {
            if (!string.IsNullOrEmpty(SubjectContains))
            {
                Query.Where(f => f.SubjectStack != null && f.SubjectStack.ToLower().Contains(SubjectContains.ToLower()));
            }
            if(IsProcessedEqual == false)
            {
                Query.Where(f => f.IsProcessed == false); 
            }

            return this;

        }
    }
}
