using System;

namespace Helpdesk.DTO
{
    public class TimerDTO
    {
        public int Id { get; set; }
        public DateTime? start { get; set; }
        public DateTime? end { get; set; }
        public int TicketId { get; set; }
        public string TicketName { get; set; }

    }
}
