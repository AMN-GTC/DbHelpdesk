using Helpdesk.Core.Common.Mailer;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace Helpdesk.Core.Entities
{
    [Table("tbl_project")]
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
        private IList<QuotaCalculation> _quotas = new List<QuotaCalculation>();
        public IList<QuotaCalculation> Quota { get => _quotas; set => _quotas = value; }


        public MailConfig ConvertToMailConfig()
        {


            List<MailConfig> mailConfigs = new List<MailConfig>();
            /*            foreach(Project project1 in project)
                        {

                        }*/
            MailConfig mailConfig = new MailConfig();
            /*foreach (var project in List<Project>)
            {
                var mailconfig = new MailConfig
                {
                    SmtpUsername = project.Sender_mail,
                    SmtpPassword = project.Password
                };
            }*/
            mailConfig.ProjectId = Id;
            mailConfig.ImapUsername = Sender_mail;
            mailConfig.ImapPassword = Password;
            mailConfig.SmtpUsername = Sender_mail;
            mailConfig.SmtpPassword = Password;

            mailConfigs.Add(mailConfig);
            return mailConfig;
        }

    }
}
