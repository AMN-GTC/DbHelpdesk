using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Helpdesk.DTO;
using Helpdesk.Infrastructure;
using Helpdesk.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VwLastWeekTicketController : ControllerBase
    {
        private readonly IVwLastWeekTicketService _vwLastWeekTicketServ;
        private readonly IMapper _mapper;
        private readonly HelpdeskDbContext _helpdeskContext;

        public VwLastWeekTicketController(IVwLastWeekTicketService vwLastWeekTicketserv, IMapper mapper, HelpdeskDbContext helpdesk)
        {
            _helpdeskContext = helpdesk;
            _vwLastWeekTicketServ = vwLastWeekTicketserv;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<VwLastWeekTicketDTO>> Get(CancellationToken cancellation = default)
        {
            IEnumerable<VwLastWeekTicket> vlwt = _helpdeskContext.VwLastWeek.ToList();
            VwLastWeekTicketSpec spec = new VwLastWeekTicketSpec();
            List<VwLastWeekTicket> listlast = await _vwLastWeekTicketServ.GetList(spec.Build(), cancellation);
            List<VwLastWeekTicketDTO> listlastDTO = _mapper.Map<List<VwLastWeekTicket>, List<VwLastWeekTicketDTO>>(listlast);
            return Ok(listlastDTO);
        }
    }
}
