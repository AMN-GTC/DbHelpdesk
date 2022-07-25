using System;
using System.Collections.Generic;
using System.Linq;
using Helpdesk.Core.Repositories;
using System.Text;
using System.Threading.Tasks;
using Helpdesk.Core.Services;
using Helpdesk.Core.Entities;
using System.Threading;
using Helpdesk.Core.Common.Mailer;
using Ardalis.Specification;
using Helpdesk.Core;
using Helpdesk.Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Helpdesk.Infrastructure.Services
{
    public abstract class ServiceEmailStack<T> : IServiceEmail /*where T : class*/
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
    public class EmailStackService : ServiceEmailStack<EmailStack>, IEmailStackService
    {
        protected IMailerService _mailerService;
        protected IProjectService _projectService;
        protected IHelpdeskUnitOfWork _emailUnitOfWork;
        protected HelpdeskDbContext _emailDBContext;

        public EmailStackService(IHelpdeskUnitOfWork emailUnitOfWork,IMailerService mailerService, IProjectService projectService, HelpdeskDbContext emailDBContext/*, DbContext context*/)
        {
            _emailUnitOfWork = emailUnitOfWork;
            _projectService = projectService;
            _mailerService = mailerService;
            _emailDBContext = emailDBContext;

/*            _dbContext = context;
            _dbSetProject = _dbContext.Set<Project>();*/
        }


        public async Task <EmailStack> Update(int id, EmailStack emailStack, CancellationToken cancellationToken = default)
        {

            await _emailUnitOfWork.emailStack.Update(emailStack, id, cancellationToken);
            await _emailUnitOfWork.SaveChangesAsync(cancellationToken);
            return emailStack;
        }
        public async Task<EmailStack> Insert(EmailStack emailStack, CancellationToken cancellationToken = default)
        {
            if (ValidateStackInsert(emailStack) == false)
            {
                return null;
            }
            
            await _emailUnitOfWork.emailStack.Insert(emailStack, cancellationToken);
            await _emailUnitOfWork.SaveChangesAsync(cancellationToken);
            return emailStack;
        }

        public async Task<List<EmailStack>> GetList(Specification<EmailStack> specification, CancellationToken cancellationToken = default)
        {
            return await _emailUnitOfWork.emailStack.GetList(specification, cancellationToken);

        }


        public async Task<EmailStack> GetObject(int id, CancellationToken cancellationToken = default)
        {
            return await _emailUnitOfWork.emailStack.GetObject(id, cancellationToken);
        }

        public async Task<bool> GetUnreadEmail(CancellationToken cancellationToken = default)
        {


            var listTicket = new List<Ticket>();
            var listStack = new List<EmailStack>();
            var emailStack = new EmailStack();

            List<Project> listAddProject = new List<Project>();
            ProjectSpesification specification = new ProjectSpesification();
            var listAddProjects = await _emailUnitOfWork.Project.GetList(specification, cancellationToken);
            foreach (var item in listAddProjects)
            {
                var spec = new TicketSpecification()
                {
                    ProjectIdequal = item.Id
                };
                var ticket = await _emailUnitOfWork.Ticket.GetList(spec.Build());


            }
            /*listAddProject = await _emailUnitOfWork.Project.GetList(specification.Build(), cancellationToken);*/
                
            foreach (var project in listAddProjects)
            {

                var projectInfo = new Project
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Sender_mail = project.Sender_mail,
                    Password = project.Password,
                    Tickets = project.Tickets
                };

                /*Sementara pakai kondisi */

                if (projectInfo.Sender_mail == "fareesahmaddd@gmail.com")
                {
                     var mecalister = await _mailerService.GetUnreadEmail(projectInfo.ConvertToMailConfig());
                     var stackConvert = ConvertListEmailToListEmailStack(mecalister);

                    /*Stack to DB*/
                    foreach (EmailStack emailStack1 in stackConvert.ToList())
                    {
                        listStack.Add(emailStack1);
                        await Insert(emailStack1, cancellationToken);
                        await _emailUnitOfWork.SaveChangesAsync(cancellationToken);

                    }
                    EmailStackSpecification emailStackSpecification = new EmailStackSpecification();
                    emailStackSpecification.IsProcessedEqual = false;
                    var listEmailStack = await GetList(emailStackSpecification.ToSpecification(), cancellationToken);

                    foreach(var emailStack2 in listEmailStack)
                    {
                        Email email = new Email();

                        if (projectInfo.Tickets.Count == 0)
                        {
                            await TicketMaker(emailStack2, projectInfo, cancellationToken);
                        }
                        else {

                            TicketSpecification ticketSpecification1 = new TicketSpecification();
                            ticketSpecification1.TitleEqual = emailStack2.SubjectStack;
                            ticketSpecification1.ProjectIdequal = emailStack2.ProjectId;
                            var objectList = await _emailUnitOfWork.Ticket.GetList(ticketSpecification1.Build(), cancellationToken);
                            if (objectList.Count == 0)
                            {
                                await TicketMaker(emailStack2, projectInfo, cancellationToken);
                            }
                        }
                    }                    
                }
            }
            return true;
        }



        public  List<EmailStack> ConvertListEmailToListEmailStack(List<Email> mails)
        {

            List<EmailStack> liststack = new List<EmailStack>();

            foreach (Email email in mails)
            {   
                var stacks = new EmailStack
                {
                        ProjectId = email.ProjectId,
                        MsgIDStack = email.MsgID,
                        SubjectStack = email.Subject,
                        FromStack = email.From,
                        ToStack = email.To,
                        BodyStack = email.Body,
                        HtmlAsBodyStack = email.HtmlAsBody,
                        MailDateTimeStack = email.MailDateTime,
                        MailDateReStack = email.MailDateRe,
                        IsProcessed = false


                };
                liststack.Add(stacks);
            }

            return liststack;

        }

        /*Jikalau servis Ticket Maker ingin dipisah*/
        public async Task<bool> TicketMaker(EmailStack emailStack, Project projectInfo,CancellationToken cancellationToken = default)
        {
            Ticket ticket = new Ticket();
            Email email = new Email();


            var ticketNewConvert = TicketToEmail2(emailStack);

            var getProject = await _emailUnitOfWork.Project.GetObject(ticketNewConvert.ProjectId, cancellationToken);
            ticketNewConvert.Project = getProject;
            ticketNewConvert.Application = getProject.Name;

            var getUser = await _emailUnitOfWork.User.GetObject(ticketNewConvert.UserId, cancellationToken);
            ticketNewConvert.User = getUser;
            ticketNewConvert.Assign_to_username = getUser.Name;

            var getStatus = await _emailUnitOfWork.Status.GetObject(ticketNewConvert.StatusId, cancellationToken);
            ticketNewConvert.TicketStatus = getStatus;
            ticketNewConvert.Ticket_status = getStatus.Name;

            await _emailUnitOfWork.Ticket.Insert(ticketNewConvert, cancellationToken);
            await _emailUnitOfWork.SaveChangesAsync(cancellationToken);

            int id = emailStack.Id;
            emailStack.IsProcessed = true;
            await _emailUnitOfWork.emailStack.Update(emailStack, id, cancellationToken);
            await _emailUnitOfWork.SaveChangesAsync(cancellationToken);

            email.Subject = ticketNewConvert.Title;
            email.TicketId = ticketNewConvert.Id;
            email.ProjectId = ticketNewConvert.ProjectId;
            email.ProjectName = ticketNewConvert.Application;
            email.To = ticketNewConvert.Reported_by;
            email.MailDateTime = ticketNewConvert.Submission_date;
            email.Body = ticketNewConvert.Problem_description;
            email.StatusTicket = ticketNewConvert.Ticket_status;

            await _mailerService.SendEmail(projectInfo.ConvertToMailConfig(), email);
            return true;
        }

        public async Task<bool> TicketMakers(List<EmailStack> emailStacks, CancellationToken cancellationToken = default)
        {
            var listTicket = new List<Ticket>();
            ProjectSpesification specification = new ProjectSpesification();
            var listAddProjects = await _emailUnitOfWork.Project.GetList(specification, cancellationToken);
            foreach (var item in listAddProjects)
            {
                var spec = new TicketSpecification()
                {
                    ProjectIdequal = item.Id
                };
                var ticket = await _emailUnitOfWork.Ticket.GetList(spec.Build());


            }
            var ticketConvert = TicketToEmail(emailStacks);


            return true;
        }
        public Ticket TicketToEmail2 (EmailStack emailStack)
        {
            Ticket ticket = new Ticket();
            
           
            ticket.ProjectId = emailStack.ProjectId;
            ticket.Title = emailStack.SubjectStack;
            ticket.Problem_description = emailStack.BodyStack;
            
            ticket.Reported_by = emailStack.FromStack;
            
            ticket.UserId = 2;
                
            ticket.StatusId = 1;
            ticket.Submission_date = DateTime.Now;

            return ticket;
        }

        public List<Ticket> TicketToEmail(List <EmailStack> emailStack)
        {
            List<Ticket> ticketemail = new List<Ticket>();

            foreach (EmailStack email in emailStack)
            {
                if (email.IsProcessed == false)
                {
                    var ticket = new Ticket
                    {
                        ProjectId = email.ProjectId,
                        Reported_by = email.FromStack,
                        UserId = 2,

                        StatusId = 1,
                        Title = email.SubjectStack,
                        Problem_description = email.BodyStack,
                        Submission_date = DateTime.Now,

                    };
                    ticketemail.Add(ticket);

                    
                    
                }


            }

            return ticketemail;
        }

        protected bool ValidateBase(EmailStack emailStack)
        {
            if(emailStack == null)
            {
                AddError("Object tersebut tidak ditemukan!");
            }
            if(string.IsNullOrEmpty(emailStack.SubjectStack))
            {
                AddError("Subject", "Subject tersebut tidk ditemukan!");
            }
            return GetServiceState();
        }

        protected bool ValidateStackInsert(EmailStack emailStack)
        {
            if (ValidateBase(emailStack) == false)
            {
                return GetServiceState();
            }
            return GetServiceState();
        }

        protected bool ValidateStackUpdate(EmailStack emailStack, int Id)
        {
            if(emailStack.Id == 0)
            {
                AddError("Id", "Id tidak boleh kosong!");
            }
            if(emailStack.Id != Id)
            {
                AddError("Id", "Id yang dimasukkan tidak dapat ditemukan!");
            }
            return GetServiceState();
        }

        protected override bool ProsesData(EmailStack emailStack)
        {
            return true;   
        }

        protected override EmailStack BindToObject(Dictionary<string, object> map)
        {
            EmailStack emailStack = new EmailStack();
            emailStack.Id = Convert.ToInt32(map["Id"]);
            return emailStack;
        }
    }
}
