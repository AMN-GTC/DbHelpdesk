using AutoMapper;
using Helpdesk.Core.Entities;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Helpdesk.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Helpdesk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusService _statusService;
        private readonly IMapper _mapper;
        public StatusController(IStatusService statusService, IMapper mapper)
        {
            _statusService = statusService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<StatusDTO>> Get(CancellationToken cancellationToken = default)
        {
            StatusSpesification specification = new StatusSpesification();
            List<Status> listStatus = await _statusService.GetList(specification.Build(), cancellationToken);
            List<StatusDTO> Statusdto = _mapper.Map<List<Status>, List<StatusDTO>>(listStatus);
            return Ok(Statusdto);
        }
    }
}
