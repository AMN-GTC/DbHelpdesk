using System;
using Helpdesk.Core;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Ardalis.Specification;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Infrastructure.Services
{
    public abstract class ServiceEmail<T> : IServiceEmail /*where T : class*/
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

        protected void ErrorClear()
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
        //error untuk proses bisnis disini
        protected void AddError(string error)
        {
            AddError("GlobalError", error);
        }

        protected abstract bool ProsesData(T emailStack);
        //contoh
        protected abstract T BindToObject(Dictionary<string, object> map);
        public void SyncData()
        {
            //baca file
            //blaaa
            Dictionary<string, object> hasilBind = new Dictionary<string, object>();
            T data = BindToObject(hasilBind);
            ProsesData(data);
        }
    }

        public class EmailService : ServiceEmail<Email>, IEmailServices
        {
            protected IHelpdeskUnitOfWork _emailUnitOfWork;
            public EmailService(IHelpdeskUnitOfWork emailUnitOfWork)
            {
                _emailUnitOfWork = emailUnitOfWork;
            }



            public async Task<bool> Delete(string id, CancellationToken cancellationToken)
            {
                await _emailUnitOfWork.email.Delete(id, cancellationToken);
                await _emailUnitOfWork.SaveChangesAsync();
                return true;

            }

            public Task<List<Email>> GetList(Specification<Email> specification, CancellationToken cancellationToken = default)
            {
                return _emailUnitOfWork.email.GetList(specification, cancellationToken);
            }

            public Task<Email> GetObject(Email emailStack, string id, CancellationToken cancellationToken = default)
            {
                return _emailUnitOfWork.email.GetObject(id, cancellationToken);
            }

            public async Task<Email> Insert(Email emailStack, CancellationToken cancellationToken = default)
            {
                if (ValidateEmailInsert(emailStack)== false)
                {
                    return null;
                }
                await _emailUnitOfWork.email.Insert(emailStack, cancellationToken);
                await _emailUnitOfWork.SaveChangesAsync();
                return emailStack;
            }

            public async Task<bool> Update(Email emailStack, string id, CancellationToken cancellationToken = default)
            {
                if(ValidateEmailUpdate(emailStack, id) == false)
                {
                    return false;
                }
                await _emailUnitOfWork.SaveChangesAsync();
                return true;
            }


            protected bool ValidateBase(Email emailStack)
            {
                if (emailStack == null)
                {
                    AddError("Object tidk boleh kosong");
                }

                if (string.IsNullOrEmpty(emailStack.Subject))
                {
                    AddError("Subject", "Subject email kosong");
                }


                if (string.IsNullOrEmpty(emailStack.MsgID))
                {
                    AddError("Body", "Body email kosong");
                }

                return GetServiceState();
            }

            protected bool ValidateEmailInsert(Email emailStack)
            {
                if (ValidateBase(emailStack) == false)
                {
                    return GetServiceState();
                }
                return GetServiceState();
            }

            protected bool ValidateEmailUpdate(Email emailStack, string id)
            {
                if (emailStack.MsgID != null)
                {
                    AddError("MsgID", "Id tidak boleh kosong.");
                }

                if (emailStack.MsgID != id)
                {
                    AddError("MsgID", "id tidak boleh diganti.");
                }

                if (string.IsNullOrEmpty(emailStack.Subject))
                {
                    AddError("Subject", "Subject kosong");
                }

                return GetServiceState();
            }

            protected override Email BindToObject(Dictionary<string, object> map)
            {
                Email emailStack = new Email();
                emailStack.MsgID = Convert.ToString(map["id"]);
                return emailStack;
            }

            protected override bool ProsesData(Email emailStack)
            {
                return true;
            }
        }
    }


