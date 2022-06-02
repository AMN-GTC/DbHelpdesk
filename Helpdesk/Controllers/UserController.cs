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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<UserDTO>> Get(CancellationToken cancellationToken = default)
        {
            UserSpesification specification = new UserSpesification();
            List<User> listuser = await _userService.GetList(specification.Build(), cancellationToken);
            List<UserDTO> listdto = _mapper.Map<List<User>, List<UserDTO>>(listuser);
            return Ok(listdto);
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Insert(UserDTO model, CancellationToken cancellationToken = default)
        {
            User user = _mapper.Map<UserDTO, User>(model);
            if (await _userService.Insert(user, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _userService.GetError();
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
    }
}
