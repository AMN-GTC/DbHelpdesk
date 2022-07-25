using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Helpdesk ;
using Helpdesk.Core.Entities;
using Helpdesk.Core;
using Helpdesk.Core.Services;
using Helpdesk.Core.Specifications;
using Helpdesk.Infrastructure;
using Helpdesk.DTO;
using Helpdesk.Core.Common.Mailer;

namespace Helpdesk.Controllers
{

    [Route("api/ReadEmail")]
    [ApiController]

    public class EmailStackController : ControllerBase
    {
        private readonly IEmailStackService _emailStackServices;

        private readonly IMapper _mapper; 

        public EmailStackController(IEmailStackService emailStackServices, IMapper mapper)
        {
            
            _emailStackServices = emailStackServices;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<EmailStackDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            EmailStackSpecification specification = new EmailStackSpecification();
            await _emailStackServices.GetUnreadEmail(cancellationToken);
            

            List<EmailStack> emailStacks = await _emailStackServices.GetList(specification.ToSpecification(), cancellationToken);
            
            List<EmailStackDTO> listStackDTO = _mapper.Map<List<EmailStack>, List<EmailStackDTO>>(emailStacks);

            
            return Ok(listStackDTO);
        }


        [HttpPost()]
        public async Task<ActionResult<EmailStackDTO>> Post([FromBody] EmailStackDTO modelStack, CancellationToken cancellationToken = default)
        {


            await _emailStackServices.GetUnreadEmail(cancellationToken);

            EmailStack emailStackMap = _mapper.Map<EmailStackDTO, EmailStack>(modelStack);

            if (await _emailStackServices.Insert(emailStackMap, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _emailStackServices.GetError();
                foreach (KeyValuePair<string, List<string>> error in errors)
                {
                    foreach (string errorvalue in error.Value)
                    {
                        ModelState.AddModelError(error.Key, errorvalue);
                    }
                }
                return BadRequest(ModelState);
            }
            EmailStackDTO result = _mapper.Map<EmailStack, EmailStackDTO>(emailStackMap);
            return Ok(result);

        }

        /* [HttpGet]
         public async Task<ActionResult<EmailStackDTO>> GetAll(CancellationToken cancellationToken = default)
         {
             EmailStackSpecification specification = new EmailStackSpecification();
             List<EmailStack> listStack = await _emailStackServices.GetList(specification.ToSpecification(), cancellationToken);
             List<EmailStackDTO> listStackDTO = _mapper.Map<List<EmailStack>, List<EmailStackDTO>>(listStack);
             return Ok(listStackDTO);

         }*/
    }
}
