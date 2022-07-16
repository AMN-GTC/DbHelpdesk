using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class TicketSpecification : Specification<Ticket>
    {
        public TicketSpecification()
        {

        }
        public Nullable<int> Idequal { get; set; }
        public string Applicationequal { get; set; }
        public string Statusequal { get; set; }
        public string Userequal { get; set; }
        public string TitleEqual { get; set; }
        public Nullable<int> ProjectIdequal { get; set; }
        public Specification<Ticket> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(f => f.Id == Idequal.Value);
            }
            if (!string.IsNullOrEmpty(Applicationequal))
            {
                Query.Where(f => f.Application != null).Include(p => p.Project.Name);
            }
            if (!string.IsNullOrEmpty(Statusequal))
            {
                Query.Where(f => f.Ticket_status.ToLower() == Statusequal.ToLower());
            }
            if (!string.IsNullOrEmpty(Userequal))
            {
                Query.Where(f => f.Assign_to_username != null).Include(p => p.User.Name);
            }
            if (ProjectIdequal.HasValue)
            {
                Query.Where(f => f.ProjectId == ProjectIdequal.Value);
            }
            if (!string.IsNullOrEmpty(TitleEqual))
            {
                Query.Where(f => f.Title.ToLower() == TitleEqual.ToLower());
            }
            return this;
        }

    }
}
