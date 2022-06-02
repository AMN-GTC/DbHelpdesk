using System;

namespace Helpdesk.Core.Entities
{
    public class TimerEntity
    {
        public int Id { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}
