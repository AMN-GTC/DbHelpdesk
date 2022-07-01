using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Helpdesk.Core;
using MimeKit;
using MimeKit.Text;
using Helpdesk.Core.Entities;

namespace Helpdesk.Infrastructure.Common.Mailer
{
    public class MailerService : IMailerService
    {
        protected IHelpdeskUnitOfWork _emailunitOfWork;
        private readonly IOptions<MailConfig> _config;

        public MailerService(IOptions<MailConfig> config,IHelpdeskUnitOfWork emailUnitOfWork)
        {
            _config = config;
            _emailunitOfWork = emailUnitOfWork;
        }



        public Task<List<Email>> GetList(Specification<Email> specification, CancellationToken cancellationToken = default)
        {
            return _emailunitOfWork.email.GetList(specification, cancellationToken);
        }


        public async Task<List<Email>> GetUnreadEmail(MailConfig mailConfig)
        {

                using (var client = new ImapClient())
                {
                    using (var cancel = new CancellationTokenSource())
                    {
                        
                        client.Connect(_config.Value.ImapServer, _config.Value.ImapPort, true, cancel.Token);


                        client.AuthenticationMechanisms.Remove("XOAUTH");

                        client.Authenticate(mailConfig.ImapUsername, mailConfig.ImapPassword, cancel.Token);


                        var inbox = client.Inbox;
                        inbox.Open(FolderAccess.ReadWrite, cancel.Token);

                        var UnreadIds = client.Inbox.Search(SearchQuery.NotSeen);

                        List<Email> emails = new List<Email>();
                    
                    foreach( var uid in UnreadIds)
                    {
                        var message = client.Inbox.GetMessage(uid);
                        if(message.InReplyTo == null)
                        {
                            Console.WriteLine(uid + "Pesan baru diterima :", message.Subject);
                            var emailMessage = new Email
                            {
                                ProjectId = mailConfig.ProjectId,
                                MsgID = message.MessageId,
                                HtmlAsBody = !string.IsNullOrEmpty(message.HtmlBody) ? message.HtmlBody : message.TextBody,
                                Subject = message.Subject,
                                Body = message.TextBody,
                                MailDateTime = message.Date.LocalDateTime,

                                From = Convert.ToString(message.From),
                                To = Convert.ToString(message.To)

                                
                            };

                            mailConfig.SmtpUsername = emailMessage.To;
                            mailConfig.SmtpUsernameTo = emailMessage.From;

                            emails.Add(emailMessage);
                            /*inbox.SetFlags(uid, MessageFlags.Seen, true);*/
                        }
                    }
                    await _emailunitOfWork.SaveChangesAsync();
                    return emails;

                        /*client.Disconnect(true, cancel.Token);*/
                    }
                }
        }

        public async Task<bool> SendEmail(MailConfig mailConfig, Email email)
        {

            var emails = new MimeMessage();

                emails.From.Add(MailboxAddress.Parse(mailConfig.SmtpUsername));
                emails.To.Add(MailboxAddress.Parse(email.To));
                emails.Subject = "Tiket dengan subject: " + email.Subject;
            if (email.ProjectName == null)
            {
                emails.Body = new TextPart(TextFormat.Html)
                {
                    Text = email.BodyConv
                };
            }
            else
            {
                emails.Body = new TextPart(TextFormat.Html)
                {
                    Text = "Hai " + email.To + ",</br></br>"
                    + "<h2>Ticket anda telah kami proses atas permohonan : " + email.Subject + "</h2>" +
                    "</br><h3>Dengan rincian sebagai berikut ; </h3></br>" +
                    "</br><h3>Ticket telah dibuat atas ;</h3>" +
                    "</br><b><h3>Aplikasi : " + email.ProjectName + "</h3></b></br>" +
                    "<b><h3> ID Aplikasi : " + email.ProjectId + "</h3></b></br>" +
                    "</br><b><h3> Dengan keterangan sebagai berikut : " +
                    email.Body + "</h3></b>"
                };
            }
                
                //format html 

                // sending email
                using var smtp = new SmtpClient();
                smtp.Connect(_config.Value.SmtpServer, _config.Value.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(mailConfig.SmtpUsername, mailConfig.SmtpPassword);
                await smtp.SendAsync(emails);
                smtp.Disconnect(true);
            
            await Task.FromResult(0);
            return true;
        }
    }
}
