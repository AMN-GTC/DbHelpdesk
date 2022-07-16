using System;

namespace Helpdesk.DTO
{
    public class VwActiveTicketSummaryDTO
    {
        public int ClosedTicket { get; set; }
        public int Late { get; set; }
        public int OpenTicket { get; set; }
        public string Project { get; set; }
        public int InProgress { get; set; }
    }
}
