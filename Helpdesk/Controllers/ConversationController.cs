using API_DB_Conversation.DTO;
using API_DB_Conversation.Entity;
using API_DB_Conversation.Repositories;
using API_DB_Conversation.Services;
using API_DB_Conversation.Specification;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace API_DB_Conversation.Controllers
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
        [HttpGet("{id}")]
        public async Task<ActionResult<ConversationDTO>> Get(int id, CancellationToken cancellationToken = default)
        {

            Conversation objek = await _conversationService.GetObject(id, cancellationToken);
            ConversationDTO ticketDTO = _mapper.Map<ConversationDTO>(objek);
            return Ok(ticketDTO);
        }
        [HttpGet]
        public async Task<ActionResult<Conversation>> Get( CancellationToken cancellationToken = default)
        {
            ConversationSpecification specification = new ConversationSpecification();
            List<Conversation> listconversation = await _conversationService.GetList(specification.Build(), cancellationToken);
            List<ConversationDTO> listdto = _mapper.Map<List<Conversation>, List<ConversationDTO>>(listconversation);
            return Ok(listdto);
        }
        [HttpPost("send")]
        public async Task<ActionResult<ConversationDTO>> Insert([FromBody]ConversationDTO model, CancellationToken cancellationToken = default)
        {

            Conversation conversation = _mapper.Map<ConversationDTO, Conversation>(model);
            await _conversationService.SendEmailAsync(conversation);
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


    }
}

