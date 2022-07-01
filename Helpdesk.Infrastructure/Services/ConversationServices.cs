using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Common.Mailer;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
{
    public class ConversationService : IConversationService
    {
        protected IHelpdeskUnitOfWork _conversationUnitOfWork;
        private readonly IMailerService _mailerService;
        private readonly IOptions<MailConfig> _mailConfig;
        public ConversationService(IOptions<MailConfig> mailConfig, IMailerService mailerService, IHelpdeskUnitOfWork conversationUnitOfWork)
        {
            _conversationUnitOfWork = conversationUnitOfWork;
            _mailerService = mailerService;
            _mailConfig = mailConfig;
        }

        public async Task<Conversation> Insert(Conversation conversation, CancellationToken cancellationToken = default)
        {

            conversation.DateTime = System.DateTime.Now;
            conversation.CreatedBy = "PT AMN Indonesia";
            await _conversationUnitOfWork.Conversation.Insert(conversation, cancellationToken);
            await _conversationUnitOfWork.SaveChangesAsync(cancellationToken);
            return conversation;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _conversationUnitOfWork.Conversation.Delete(id, cancellationToken);
            await _conversationUnitOfWork.SaveChangesAsync();
            return true;
        }

        public Dictionary<string, List<string>> GetError()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancellationToken = default)
        {
           
            return _conversationUnitOfWork.Conversation.GetList(specification, cancellationToken);
        }

        public Task<Conversation> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _conversationUnitOfWork.Conversation.GetObject(id, cancellationToken);
        }

        public bool GetServiceState()
        {
            throw new System.NotImplementedException();
        }
       /* public async Task SendEmailAsync(Conversation conversation, CancellationToken cancellationToken = default)
        {
            //membuat email baru
            var email = new MimeMessage();
            //inisialisasi profil pengirim email dengan mengambil entity dari file mail settings
            email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
            //inisialisasi profil penerima email dari appsettingsjson
            email.To.Add(MailboxAddress.Parse(conversation.ToEmail));
            //inisialisasi subject dari file conversation
            email.Subject = conversation.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = conversation.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            //inisialisasi untuk connect ke server smpt dengan host dan port ambil dari appsettings.json
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            //inisialisasi untuk connect ke akun gmail dengan ambil password dari appsettings.json dari variable Mail dan Password
            smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
            //perintah untuk kirim email
            await smtp.SendAsync(email);
            //setelah kirim email disconnect smtp
            smtp.Disconnect(true);
        }
*/

        public Conversation ConvertProjectToConversation(Project project)
        {
            Conversation conversation = new Conversation();
            conversation.ProjectId = project.Id;
            conversation.ToEmail = project.Sender_mail;
            return conversation;
        }

        public Email ConvertConversationToEmail(Conversation conversation)
        {
            Email email = new Email();
            email.To = conversation.ToEmail;
            email.From = conversation.CreatedBy;
            email.Subject = conversation.Subject;
            email.BodyConv = conversation.Body;
            email.MailDateTime = conversation.DateTime;
            return email;
        }
        public MailConfig ConvertConversationinfoToMailCOnfig(Conversation conversation)
        {
            MailConfig mailConfig = new MailConfig();
            mailConfig.SmtpUsernameTo = conversation.ToEmail;
            return mailConfig;
        }

        public async Task<Conversation> SendConversation(Conversation conversation, CancellationToken cancellationToken = default)
        {
            // Load Data ProjectInfo terkait conversation.
            var project = await _conversationUnitOfWork.Project.GetObject(conversation.ProjectId, cancellationToken);
            conversation.CreatedBy = project.Sender_mail;
            conversation.Password = project.Password;
            conversation.DateTime = DateTime.Now;
            var mailConfig = project.ConvertToMailConfig();
            await _conversationUnitOfWork.Conversation.Insert(conversation, cancellationToken);
            await _conversationUnitOfWork.SaveChangesAsync(cancellationToken);
            var email = ConvertConversationToEmail(conversation);
            await _mailerService.SendEmail(mailConfig, email);
            
            return conversation;
        }
        public EmailStack BindStackEmailToConversation(Email email)
        {
            //Lakukan proses convert type Email ke type StackEmail
            EmailStack emailStack = new EmailStack();
            emailStack.ToStack = email.To;
            emailStack.FromStack = email.From;
            emailStack.BodyStack = email.Body;
            emailStack.SubjectStack = email.Subject;
            emailStack.MailDateReStack = email.MailDateRe;
            emailStack.MailDateTimeStack = email.MailDateTime;
            emailStack.HtmlAsBodyStack = email.HtmlAsBody;
            emailStack.MsgIDStack = email.MsgID;
            emailStack.MsgThreadIDStack = email.MsgThreadID;
            return emailStack;
        }
        public Conversation ConvertEmailStackToConversation(EmailStack stack)
        {
            Conversation conversation = new Conversation();
            conversation.CreatedBy = stack.ToStack;
            conversation.Subject = stack.SubjectStack;
            conversation.Body = stack.BodyStack;
            conversation.DateTime = stack.MailDateTimeStack;
            conversation.ToEmail = stack.FromStack;
            return conversation;
        }
        public async Task<bool> GetConversationFromEmail(CancellationToken cancellationToken = default)
        {
            //Load data List StackInfo dengan kondisi IsProcessed = false
            List<EmailStack> listStack = new List<EmailStack>();
            EmailStackSpecification specification = new EmailStackSpecification();
            listStack = await _conversationUnitOfWork.emailStack.GetList(specification, cancellationToken);
            //Loop data List StackInfo
            foreach (EmailStack emailStack in listStack)
            {
                //Jika mengandung tiket number maka convert ke conversation.
                if (emailStack.IsProcessed == false)
                {
                    var convert = ConvertEmailStackToConversation(emailStack);
                    //Insert Conversation.
                    await _conversationUnitOfWork.Conversation.Insert(convert, cancellationToken);
                    await _conversationUnitOfWork.SaveChangesAsync(cancellationToken);
                }
            }
            return true;
        }
        public async Task<List<Conversation>> GetConversations(Specification<Conversation> specification, CancellationToken cancellationToken)
        {
            //Implementasikan proses get data conversation
            await GetConversationFromEmail(cancellationToken);
            return await _conversationUnitOfWork.Conversation.GetList(specification, cancellationToken);
        }

        public async Task<List<Conversation>> GetConversationTicketId(int ticketId, CancellationToken cancellationToken)
        {
            var spec = new ConversationSpecification();
            spec.Idequal = ticketId;
            await GetConversationFromEmail(cancellationToken);
            return await _conversationUnitOfWork.Conversation.GetList(spec.Build(), cancellationToken);
        }
    }
    
}
