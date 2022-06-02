using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
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
    public class VwTicketPICController : ControllerBase
    {
        private readonly IVwTicketPICServ _vwTicketPICServ;
        private readonly IMapper _mapper;
        private readonly HelpdeskDbContext _helpdeskContext;

        public VwTicketPICController(IVwTicketPICServ vwTicketPICServ, IMapper mapper, HelpdeskDbContext helpdesk)
        {
            _helpdeskContext = helpdesk;
            _vwTicketPICServ = vwTicketPICServ;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<VwTicketPICDTO>> Get(CancellationToken cancellation = default)
        {
            IEnumerable<VwTicketPIC> vwpic = _helpdeskContext.VwTicketPIC.ToList();
            VwTicketPICSpec spec = new VwTicketPICSpec();
            List<VwTicketPIC> listpic = await _vwTicketPICServ.GetList(spec.Build(), cancellation);
            List<VwTicketPICDTO> listpicDTO = _mapper.Map<List<VwTicketPIC>, List<VwTicketPICDTO>>(listpic);
            return Ok(listpicDTO);
        }
    }
}
