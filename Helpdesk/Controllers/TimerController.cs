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
    public class TimerController : ControllerBase
    {
        private readonly ITimerService _timerService;
        private readonly IMapper _mapper;
        public TimerController(ITimerService timerService, IMapper mapper)
        {
            _timerService = timerService;
            _mapper = mapper;
        }

        [HttpGet("StartTimer")]
        public async Task<ActionResult<TimerDTO>> StartTimer(int ticketId, CancellationToken cancellationToken = default)
        {
            TimerEntity timer = await _timerService.StartTimer(ticketId, cancellationToken);
            TimerDTO timerDTO = _mapper.Map<TimerDTO>(timer);
            Dictionary<string, List<string>> errors = _timerService.GetError();
            foreach (KeyValuePair<string, List<string>> error in errors)
            {
                foreach (string errorValue in error.Value)
                {
                    ModelState.AddModelError(error.Key, errorValue);
                }
                return BadRequest(ModelState);
            }
            return Ok(timerDTO);

        }

        [HttpGet("StopTimer")]
        public async Task<ActionResult<TimerDTO>> StopTimer(int ticketId, CancellationToken cancellationToken = default)
        {
            TimerEntity timer = await _timerService.StopTimer(ticketId, cancellationToken);
            TimerDTO timerDTO = _mapper.Map<TimerDTO>(timer);
            Dictionary<string, List<string>> errors = _timerService.GetError();
            foreach (KeyValuePair<string, List<string>> error in errors)
            {
                foreach (string errorValue in error.Value)
                {
                    ModelState.AddModelError(error.Key, errorValue);
                }
                return BadRequest(ModelState);
            }
            return Ok(timerDTO);
        }

        [HttpGet("ListTimer")]
        public async Task<ActionResult<TimerDTO>> Get(CancellationToken cancellationToken = default)
        {

            TimerSpesification specification = new TimerSpesification();
            List<TimerEntity> listtimer = await _timerService.GetList(specification.Build(), cancellationToken);
            List<TimerDTO> listdto = _mapper.Map<List<TimerEntity>, List<TimerDTO>>(listtimer);
            return Ok(listdto);
        }
        [HttpGet]
        public async Task<ActionResult<TimerDTO>> Get([FromQuery] Dictionary<string, string> filter, CancellationToken cancellationToken = default)
        {

            TimerSpesification specification = new TimerSpesification();
            if (filter.ContainsKey("TicketidEqual"))
            {
                specification.Ticketid = int.Parse(filter["TicketidEqual"]);
            }
            List<TimerEntity> listtimer = await _timerService.GetList(specification.Build(), cancellationToken);
            List<TimerDTO> listdto = _mapper.Map<List<TimerEntity>, List<TimerDTO>>(listtimer);
            return Ok(listdto);
        }

        [HttpPost]
        public async Task<ActionResult<TimerDTO>> Insert(TimerDTO model, CancellationToken cancellationToken = default)
        {
            TimerEntity timerentity = _mapper.Map<TimerDTO, TimerEntity>(model);
            if (await _timerService.Insert(timerentity, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _timerService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorValue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorValue);
                    }
                }
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] TimerDTO model, int id, CancellationToken cancellationToken = default)
        {
            TimerEntity timerEntity = _mapper.Map<TimerDTO, TimerEntity>(model);
            if (await _timerService.Update(timerEntity, id, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _timerService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            TimerDTO result = _mapper.Map<TimerEntity, TimerDTO>(timerEntity);
            return Ok(result);
        }
    }

}

