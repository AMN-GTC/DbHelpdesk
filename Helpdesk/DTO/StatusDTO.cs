using System.Collections.Generic;

namespace Helpdesk.DTO
{
    public class StatusDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();
    }
}
