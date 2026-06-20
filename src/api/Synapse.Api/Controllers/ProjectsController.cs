using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Synapse.Application.Features.Projects;
using Synapse.Domain.Entities;
using Synapse.Infrastructure.Persistence;

namespace Synapse.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectsController : ControllerBase
    {
        public readonly IProjectService _projectService;

        public ProjectsController (IProjectService projectService)
        {
            _projectService = projectService;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            var projects = await _projectService.GetAllAsync();

            return Ok(projects);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<Project>> Create(CreateProjectDto project)
        {
            if (project is null)
                return BadRequest();

            _projectService?.CreateAsync(project);

            return Ok();
        }
        #endregion
    }
}
