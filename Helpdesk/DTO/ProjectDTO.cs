using System.Collections.Generic;

namespace Helpdesk.DTO
{
    public class ProjectDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sender_mail { get; set; }
        public string Sender_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();


    }
}
