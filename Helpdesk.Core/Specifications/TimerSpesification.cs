using Ardalis.Specification;
using Helpdesk.Core.Entities;
using System;

namespace Helpdesk.Core.Specifications
{
    public class TimerSpesification : Specification<TimerEntity>
    {
        public TimerSpesification()
        {

        }
        public Nullable<int> Idequal { get; set; }
        public Nullable<int> TicketidEqual { get; set; }
        public bool? IsEndNull { get; set; }

        public Nullable<int> Ticketid { get; set; }


        public Specification<TimerEntity> Build()
        {
            if (Idequal.HasValue)
            {
                Query.Where(x => x.Id == Idequal.Value);
            }
            if (TicketidEqual.HasValue)
            {
                Query.Where(x => x.TicketId == TicketidEqual.Value);
            }
            if (IsEndNull.HasValue)
            {
                if (IsEndNull == true)
                {
                    Query.Where(f => f.end == null);
                }
                else
                {
                    Query.Where(f => f.end != null);
                }

            }

            if (Ticketid.HasValue)
            {
                Query.Where(f => f.TicketId == f.Ticket.Id);
            }

            return this;
        }

    }

}
