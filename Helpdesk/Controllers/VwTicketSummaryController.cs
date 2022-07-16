using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
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
    public class VwTicketSummaryController : ControllerBase
    {
        private readonly IVwTicketSummaryServ _IVwTicketSummaryService;
        private readonly IMapper _mapper;
        private readonly HelpdeskDbContext _helpdeskContext;

        public VwTicketSummaryController(IVwTicketSummaryServ vwActiveSummaryServices, IMapper mapper, HelpdeskDbContext helpdesk)
        {
            _helpdeskContext = helpdesk;
            _IVwTicketSummaryService = vwActiveSummaryServices;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<VwTicketSummaryDTO>> Get(CancellationToken cancellation = default)
        {
            IEnumerable<VwTicketSummary> vws = _helpdeskContext.VwSumChartTicketSumm.ToList();
            VwTicketSummarySpec spec = new VwTicketSummarySpec();
            List<VwTicketSummary> listVw = await _IVwTicketSummaryService.GetList(spec.Build(), cancellation);
            List<VwTicketSummaryDTO> listVwDTO = _mapper.Map<List<VwTicketSummary>, List<VwTicketSummaryDTO>>(listVw);
            return Ok(listVwDTO);
        }
    }
}
