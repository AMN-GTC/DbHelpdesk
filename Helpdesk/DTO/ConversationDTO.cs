using System;

namespace API_DB_Conversation.DTO
{
    public class ConversationDTO
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateTime { get; set; }
        public string CreatedBy { get; set; }
    }
}
