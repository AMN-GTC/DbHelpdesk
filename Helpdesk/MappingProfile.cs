using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.DTO;

namespace Helpdesk
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            base.CreateMap<EmailStack, EmailStackDTO>().ReverseMap();
            base.CreateMap<VwQuota, VwQuotaDTO>().ReverseMap();
            base.CreateMap<VwLastWeekTicket, VwLastWeekTicketDTO>().ReverseMap();
            base.CreateMap<VwTicketSummary, VwTicketSummaryDTO>().ReverseMap();
            base.CreateMap<VwTicketPIC, VwTicketPICDTO>().ReverseMap();
            base.CreateMap<VwActiveTicketSummary, VwActiveTicketSummaryDTO>().ReverseMap();
            base.CreateMap<User, UserDTO>().ReverseMap();
            base.CreateMap<Status, StatusDTO>().ReverseMap();
            base.CreateMap<Conversation, ConversationDTO>().ReverseMap();

            base.CreateMap<Ticket, TicketDTO>()
                .ForMember(a => a.Assign_to_username, opt => opt.MapFrom(n => n.User.Name))
                .ForMember(a => a.Application, opt => opt.MapFrom(n => n.Project.Name))
                .ForMember(a => a.Ticket_status, opt => opt.MapFrom(n => n.TicketStatus.Name))
                .ReverseMap()
                .ForMember(p => p.Project, opt => opt.Ignore())
                .ForMember(s => s.TicketStatus, opt => opt.Ignore())
                .ForMember(u => u.User, opt => opt.Ignore());


            base.CreateMap<TimerEntity, TimerDTO>()
                .ForMember(t => t.TicketName, opt => opt.MapFrom(n => n.Ticket.Title)).ReverseMap()
                .ForMember(t => t.Ticket, opt => opt.Ignore());
            base.CreateMap<Project, ProjectDTO>().ReverseMap()
                .ForMember(t => t.Tickets, opt => opt.Ignore());


        }
    }
}
