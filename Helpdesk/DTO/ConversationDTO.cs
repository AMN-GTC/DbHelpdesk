using System;

namespace Helpdesk.DTO
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public int ProjectId { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public string CreatedBy { get; set; }
        public string Password { get; set; }
    }
}
