using Helpdesk.Core.Entities;
using Helpdesk.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Helpdesk.Core.Common.Mailer
{
	public class MailConfig 
	{
		public const string EmailConfiguration = "EmailConfiguration";

		public int ProjectId { get; set; }
		public string SmtpServer { get; set; }
		public int SmtpPort { get; set; }
		public string SmtpUsername { get; set; }
		public string SmtpUsernameTo { get; set; }
		public string SmtpPassword { get; set; }
		public string ImapServer { get; set; }
		public int ImapPort { get; set; }
		public string ImapUsername { get; set; }
		public string ImapPassword { get; set; }


	}
}
