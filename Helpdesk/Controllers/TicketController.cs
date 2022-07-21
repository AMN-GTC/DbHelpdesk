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
    public class TicketController : ControllerBase
    {
            
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;
        public TicketController(ITicketService ticketService, IMapper mapper)
        {
            _ticketService = ticketService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<TicketDTO>> Get(CancellationToken cancellationToken = default)
        {
            TicketSpecification specification = new TicketSpecification();
            List<Ticket> listticket = await _ticketService.GetList(specification.Build(), cancellationToken);
            List<TicketDTO> listdto = _mapper.Map<List<Ticket>, List<TicketDTO>>(listticket);
            return Ok(listdto);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDTO>> Get(int id, CancellationToken cancellationToken = default)
        {

            Ticket objek = await _ticketService.GetObject(id, cancellationToken);
            TicketDTO ticketDTO = _mapper.Map<TicketDTO>(objek);
            return Ok(ticketDTO);
        }


        [HttpPost()]
        public async Task<ActionResult<TicketDTO>> Insert([FromBody] TicketDTO model, CancellationToken cancellationToken = default)
        {
            Ticket ticket = _mapper.Map<TicketDTO, Ticket>(model);
            if (await _ticketService.Insert(ticket, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _ticketService.GetError();
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
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken = default)
        {

            if (await _ticketService.Delete(id, cancellationToken) == false)
            {
                Dictionary<string, List<string>> errors = _ticketService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok("berhasil dihapus");
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromBody] TicketDTO model, int id, CancellationToken cancellationToken = default)
        {
            Ticket ticket = _mapper.Map<TicketDTO, Ticket>(model);
            if (await _ticketService.Update(ticket, id, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _ticketService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            TicketDTO result = _mapper.Map<Ticket, TicketDTO>(ticket);
            return Ok(result);
        }
    }
}

