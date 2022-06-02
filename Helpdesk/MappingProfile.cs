using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.DTO;

namespace Helpdesk
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            base.CreateMap<VwTicketSummary, VwTicketSummaryDTO>().ReverseMap();
            base.CreateMap<VwTicketPIC, VwTicketPICDTO>().ReverseMap();
            base.CreateMap<VwActiveTicketSummary, VwActiveTicketSummaryDTO>().ReverseMap();
        }
    }
}
