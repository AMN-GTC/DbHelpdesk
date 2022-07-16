using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Specifications;
using Helpdesk.DTO;
using Helpdesk.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VwQuotaController : ControllerBase
    {
        private readonly IVwQuotaServ _vwQuotaServ;
        private readonly IMapper _mapper;
        private readonly HelpdeskDbContext _helpdeskDbContext;

        public VwQuotaController(IVwQuotaServ vwQuotaServ, IMapper mapper, HelpdeskDbContext helpdeskDbContext)
        {
            _vwQuotaServ = vwQuotaServ;
            _mapper = mapper;
            _helpdeskDbContext = helpdeskDbContext;
        }
        [HttpGet]
        public async Task<ActionResult<VwQuotaDTO>> Get(CancellationToken cancellationToken = default)
        {
            IEnumerable<VwQuota> vwquota = _helpdeskDbContext.VwQuotaSummary.ToList();
            VwQuotaSpec spec = new VwQuotaSpec();
            List<VwQuota> listquota = await  _vwQuotaServ.GetList(spec.Build(), cancellationToken);
            List<VwQuotaDTO> listquotadto = _mapper.Map<List<VwQuota>, List<VwQuotaDTO>>(listquota);
            return Ok(listquotadto);
        }
    }
}
