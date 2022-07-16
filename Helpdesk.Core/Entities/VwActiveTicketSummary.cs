
using System;

namespace Helpdesk.Core.Entities
{
    public class VwActiveTicketSummary
    {
        public int ClosedTicket { get; set; }
        public int Late { get; set; }
        public int OpenTicket { get; set; }
        public string Project { get; set; }
        public int InProgress { get; set;}
    }
}
