using System.Collections.Generic;

namespace Helpdesk.Core.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        private IList<Ticket> _tickets = new List<Ticket>();
        public IList<Ticket> Tickets { get => _tickets; set => _tickets = value; }
        //private List<Ticket> _Ticket { get; set; }
        //public IReadOnlyList<Ticket> ticket => _Ticket;

    }
}
