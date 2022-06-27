using API_DB_Ticket.Entity;
using API_DB_Ticket.Settings;
using API_DB_Ticket.Specification;
using API_DB_Ticket.UnitOfWork;
using Ardalis.Specification;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API_DB_Ticket.Services
{
    public abstract class ServiceConversation<T> : IService
    {
        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> GetError()
        {
            return _errors;
        }

        public bool GetServiceState()
        {
            return _errors.Count == 0;
        }
        protected void ClearError()
        {
            _errors.Clear();
        }
        protected void AddError(string key, string error)
        {
            if (_errors.ContainsKey(key) == false)
            {
                _errors.Add(key, new List<string>());
            }
            _errors[key].Add(error);
        }
        protected void AddError(string error)
        {
            AddError("global error", error);
        }
    }
    public class ConversationService : IConversationService
    {
        protected ITicketUnitOfWork _conversationUnitOfWork;
        private readonly IMailerService _mailerService;
        private readonly MailSettings _mailSettings;
        private readonly IOptions<MailConfig> _mailConfig;
        public ConversationService(IOptions<MailConfig> mailConfig, IMailerService mailerService, ITicketUnitOfWork conversationUnitOfWork, IOptions<MailSettings> mailSettings)
        {
            _conversationUnitOfWork = conversationUnitOfWork;
            _mailSettings = mailSettings.Value;
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
        public async Task SendEmailAsync(Conversation conversation, CancellationToken cancellationToken = default)
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

        public async Task<Conversation> SendConversation(Conversation conversation, CancellationToken cancellationToken = default)
        {
            //Load Data ProjectInfo terkait conversation. 
            Project projectinfo = new Project();
            ProjectSpesification specification = new ProjectSpesification();
            var listprojectinfo = await _conversationUnitOfWork.Project.GetList(specification, cancellationToken);
            foreach (Project project in listprojectinfo)
            {
                projectinfo.Id = project.Id;
                projectinfo.Sender_mail = project.Sender_mail;
            }
            listprojectinfo.Add(projectinfo);
            conversation.ProjectId = projectinfo.Id;
            conversation.ToEmail = projectinfo.Sender_mail;
            //Simpan data conversation ke database.
            await _conversationUnitOfWork.Conversation.Insert(conversation, cancellationToken);
            await _conversationUnitOfWork.SaveChangesAsync(cancellationToken);
            await SendEmailAsync(conversation, cancellationToken);
            //return conversation
            return conversation;
        }
        /*public Conversation BindStackEmailToConversation(Email email)
        {
            //Lakukan proses convert type Email ke type StackEmail
            List<EmailStack> emailStacks = new List<EmailStack>();
            EmailStack emailStack = new EmailStack();

            emailStack.FromStack = email.From;
            emailStack.ToStack = email.To;
            emailStack.SubjectStack = email.Subject;
            emailStack.HtmlAsBodyStack = email.HtmlAsBody;
            emailStack.BodyStack = email.Body;
            emailStack.MailDateTimeStack = email.MailDateTime;
            emailStack.MsgIDStack = email.MsgID;
            emailStack.MsgThreadIDStack = email.MsgThreadID;
            emailStack.MailDateReStack = email.MailDateRe;

            emailStacks.Add(emailStack);
            return emailStack;
        }*/
        public EmailStack BindStackEmailToConversation(Email email)
        {
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
        public async Task<bool> GetConversationFromEmail(CancellationToken cancellationToken = default)
        {
            List<EmailStack> listStack = new List<EmailStack>();
            foreach (EmailStack emailStack in listStack) 
            {
                if (emailStack.IsProcessed == false)
                {
                    EmailStackSpecification specification = new EmailStackSpecification();
                    listStack = await _conversationUnitOfWork.emailStack.GetList(specification, cancellationToken);
                    var stackinfo = new EmailStack
                    {
                        Id = emailStack.Id,
                        IsProcessed = emailStack.IsProcessed,
                        FromStack = emailStack.FromStack,
                        ToStack = emailStack.ToStack,
                        SubjectStack = emailStack.SubjectStack,
                        HtmlAsBodyStack = emailStack.HtmlAsBodyStack,
                        BodyStack = emailStack.BodyStack,
                        MailDateTimeStack = emailStack.MailDateTimeStack,
                        MsgIDStack = emailStack.MsgIDStack,
                        MsgThreadIDStack = emailStack.MsgThreadIDStack,
                        MailDateReStack = emailStack.MailDateReStack
                    };
                    listStack.Add(stackinfo);
                }
            }
            Conversation conversation = new Conversation();
            List<Conversation> conversations = new List<Conversation>();
            foreach (EmailStack stackinfo in listStack)
            {
                conversation.CreatedBy = stackinfo.ToStack;
                conversation.Subject = stackinfo.SubjectStack;
                conversation.Body = stackinfo.BodyStack;
                conversation.DateTime = stackinfo.MailDateTimeStack;
                conversation.ToEmail = stackinfo.FromStack;     
            }
            conversations.Add(conversation);
            await _conversationUnitOfWork.Conversation.Insert(conversation, cancellationToken);
            await _conversationUnitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        public async Task<IReadOnlyList<Conversation>> GetConversations(Specification<Conversation> filter)
        {
            var list = new List<Conversation>();
            var convert =  await _conversationUnitOfWork.Conversation.GetList(filter);
            return convert;
        }

    }
}

