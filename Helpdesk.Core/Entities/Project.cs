using System.Collections.Generic;

namespace Helpdesk.Core.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Sender_mail { get; set; }
        public string Sender_name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        private IList<Ticket> _tickets = new List<Ticket>();
        public IList<Ticket> Tickets { get => _tickets; set => _tickets = value; }

    }
}
