using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpdesk.Core.Common.Mailer
{
    public class Email
    {
        public string To { get; set; }
        public string From { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int TicketId { get; set; }
        public string Subject { get; set; }
        public string HtmlAsBody { get; set; }
        public string Body { get; set; }
        public string BodyConv {get; set;}
        public DateTime MailDateTime { get; set; }
        public string MsgID { get; set; }
        public string MsgThreadID { get; set; }
        public DateTime MailDateRe { get; set; }
        public string StatusTicket { get; set; }


    }
}
