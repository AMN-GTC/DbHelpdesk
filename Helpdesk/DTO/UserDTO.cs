using System.Collections.Generic;

namespace Helpdesk.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();
    }
}
