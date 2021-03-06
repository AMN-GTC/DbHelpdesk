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
    public class ConversationController : ControllerBase
    {

        private readonly IConversationService _conversationService;
        private readonly IMapper _mapper;
        public ConversationController(IConversationService conversationService, IMapper mapper)
        {
            _conversationService = conversationService;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Conversation>> Get(CancellationToken cancellationToken = default)
        {
            ConversationSpecification specification = new ConversationSpecification();
            List<Conversation> listconversation = await _conversationService.GetList(specification.Build(), cancellationToken);
            List<ConversationDTO> listdto = _mapper.Map<List<Conversation>, List<ConversationDTO>>(listconversation);
            return Ok(listdto);
        }
        /*[HttpGet("{id}")]
        public async Task<ActionResult<ConversationDTO>> Get(int id, CancellationToken cancellationToken = default)
        {
            Conversation objek = await _conversationService.GetObject(id, cancellationToken);
            ConversationDTO conversationDTO = _mapper.Map<ConversationDTO>(objek);
            return Ok(conversationDTO);
        }*/

        [HttpPost("send")]
        public async Task<ActionResult<ConversationDTO>> Send([FromBody] ConversationDTO model, CancellationToken cancellationToken = default)
        {

            Conversation conversation = _mapper.Map<ConversationDTO, Conversation>(model);
            //await _conversationService.SendEmailAsync(conversation);
            if (await _conversationService.Insert(conversation, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _conversationService.GetError();
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

            if (await _conversationService.Delete(id, cancellationToken) == false)
            {
                Dictionary<string, List<string>> errors = _conversationService.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            return Ok("Anjay ke hapus euy");
        }

        [HttpGet("Get Conversation")]
        public async Task<IEnumerable<Conversation>> Getting(CancellationToken cancellationToken)
        {
            //Convert filter dari parameter ke bentuk ConversationFilter
            ConversationSpecification specification = new ConversationSpecification();
            List<Conversation> listconversation = await _conversationService.GetConversations(specification.Build(), cancellationToken);
            List<Conversation> list = _mapper.Map<List<Conversation>>(listconversation);
            //Return data list.
            return list;
        }

        [HttpPost("send dari faris")]
        public async Task<ActionResult> Insert([FromBody] ConversationDTO dto, CancellationToken cancellationToken = default)
        {
            //Convert DTO menjadi Entity
            Conversation conversation = _mapper.Map<ConversationDTO, Conversation>(dto);
            //Panggil method yang ada di IConversationService.SendConversation(Conversation model, CancellationToken cancellationToken).
            await _conversationService.SendConversation(conversation);
            return Ok();
        }
        [HttpGet("{ticketId}")]
        public async Task<IEnumerable<Conversation>> Getttings(int ticketId ,CancellationToken cancellationToken = default)
        {
            //Convert filter dari parameter ke bentuk ConversationFilter
            ConversationSpecification specification = new ConversationSpecification();
            List<Conversation> listconversation = await _conversationService.GetConversationTicketId(ticketId, cancellationToken);
            List<Conversation> list = _mapper.Map<List<Conversation>>(listconversation);
            //Return data list.
            return list;
        }



    }
}
