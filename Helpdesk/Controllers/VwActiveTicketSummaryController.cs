using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Specifications.Filters;
using Helpdesk.DTO;
using Helpdesk.Infrastructure;
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
    public class VwActiveTicketSummaryController : ControllerBase
    {
        private readonly IVwActiveTicketSummaryServ _vwActiveTicketSummaryServ;
        private readonly IMapper _mapper;
        private readonly HelpdeskDbContext _helpdeskContext;

        public VwActiveTicketSummaryController(IVwActiveTicketSummaryServ vwActiveTicketSummaryServ, IMapper mapper, HelpdeskDbContext helpdesk)
        {
            _helpdeskContext = helpdesk;
            _vwActiveTicketSummaryServ = vwActiveTicketSummaryServ;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<VwActiveTicketSummaryDTO>> Get(CancellationToken cancellation = default)
        {
            IEnumerable<VwActiveTicketSummary> vwpic = _helpdeskContext.VwActiveTicketSummaries.ToList();
            VwActiveTicketSummarySpec spec = new VwActiveTicketSummarySpec();
            List<VwActiveTicketSummary> listvwats = await _vwActiveTicketSummaryServ.GetList(spec.Build(), cancellation);
            List<VwActiveTicketSummaryDTO> listvwatsDTO = _mapper.Map<List<VwActiveTicketSummary>, List<VwActiveTicketSummaryDTO>>(listvwats);
            return Ok(listvwatsDTO);
        }

    }
}
