using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Infrastructure.Services
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
        protected IHelpdeskUnitOfWork _helpdeskUnitOfWork;
        private readonly MailSettings _mailSettings;

        public ConversationService(IHelpdeskUnitOfWork helpdeskUnitOfWork, IOptions<MailSettings> mailSettings)
        {
            _helpdeskUnitOfWork = helpdeskUnitOfWork;
            _mailSettings = mailSettings.Value;
        }

        public async Task<Conversation> Insert(Conversation conversation, CancellationToken cancellationToken = default)
        {
            conversation.DateTime = System.DateTime.Now;
            conversation.CreatedBy = "PT AMN Indonesia";
            await _helpdeskUnitOfWork.Conversation.Insert(conversation, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync(cancellationToken);
            return conversation;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
        {
            await _helpdeskUnitOfWork.Conversation.Delete(id, cancellationToken);
            await _helpdeskUnitOfWork.SaveChangesAsync();
            return true;
        }

        public Dictionary<string, List<string>> GetError()
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Conversation>> GetList(Specification<Conversation> specification, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Conversation.GetList(specification, cancellationToken);
        }

        public Task<Conversation> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return _helpdeskUnitOfWork.Conversation.GetObject(id, cancellationToken);
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
            //inisialisasi subject dari file conversation, nanti diisikan di API
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



    }
}
