using Helpdesk.Core.Entities;
using System;

namespace Helpdesk
{
    public class EmailStackDTO
    {
        public int Id { get; set; }
        public bool IsProcessed { get; set; }
        public int ProjectId { get; set; }
        public string FromStack { get; set; }
        public string ToStack { get; set; }
        public string SubjectStack { get; set; }
        public string HtmlAsBodyStack { get; set; }
        public string BodyStack { get; set; }
        public DateTime MailDateTimeStack { get; set; }
        public string MsgIDStack { get; set; }
        public string MsgThreadIDStack { get; set; }
        public DateTime MailDateReStack { get; set; }
    }
}
