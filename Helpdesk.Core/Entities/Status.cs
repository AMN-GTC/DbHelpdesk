using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Core.Entities
{
    [Table("tbl_TicketStatus")]
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private IList<Ticket> _tickets = new List<Ticket>();
        public IList<Ticket> Tickets { get => _tickets; set => _tickets = value; }
        //private List<Ticket> _Ticket { get; set; }
        //public IReadOnlyList<Ticket> ticket => _Ticket;
    }
}
