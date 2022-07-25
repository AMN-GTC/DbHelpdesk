using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;
using Helpdesk.Core.Entities;


namespace Helpdesk.Core.Specifications
{
    public class EmailSpecification : Specification <Email>
    {
        public EmailSpecification()
        {

        }

        public string MsgIdequal { get; set; }    

        public Nullable<int> Idequal { get; set; }
        public string Subjectequal { get; set; }
        public string SubjectContains { get; set; } 


       public Specification<Email> Build()
        {
            
            if (!string.IsNullOrEmpty(SubjectContains))
            {
                Query.Where(f => f.Subject != null && f.Subject.ToLower().Contains(SubjectContains.ToLower()));
            }
            if (!string.IsNullOrEmpty(Subjectequal))
            {
                Query.Where(f => f.Subject != null && f.Subject.ToLower() == Subjectequal.ToLower());
            }

            return this;
        }

    }
}
