using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helpdesk.Core.Entities
{
    [Table("tbl_Timer")]
    public class TimerEntity
    {
        public int Id { get; set; }
        public Nullable<DateTime> start { get; set; }
        public Nullable<DateTime> end { get; set; }
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}
