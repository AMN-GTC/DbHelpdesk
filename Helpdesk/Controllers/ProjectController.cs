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
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMapper _mapper;
        public ProjectController(IProjectService projectService, IMapper mapper)
        {
            _projectService = projectService;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<ProjectDTO>> Get(CancellationToken cancellationToken = default)
        {
            ProjectSpesification specification = new ProjectSpesification();
            List<Project> listproject = await _projectService.GetList(specification.Build(), cancellationToken);
            List<ProjectDTO> projectdto = _mapper.Map<List<Project>, List<ProjectDTO>>(listproject);
            return Ok(projectdto);
        }
        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Insert([FromBody] ProjectDTO model, CancellationToken cancellationToken = default)
        {
            Project project = _mapper.Map<ProjectDTO, Project>(model);
            if (await _projectService.Insert(project, cancellationToken) == null)
            {
                Dictionary<string, List<string>> errors = _projectService.GetError();
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
