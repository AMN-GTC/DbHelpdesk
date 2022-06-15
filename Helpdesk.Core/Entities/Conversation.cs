using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Core.Entities
{
        [Table("tbl_conversation")]
        public class Conversation
        {
            [Key]
            public int Id { get; set; }
            public int TicketId { get; set; }
            public string ToEmail { get; set; }
            public string Subject { get; set; }
            public string Body { get; set; }
            public DateTime DateTime { get; set; }
            public string CreatedBy { get; set; }

        }
    }
