using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Synapse.Domain.Entities;
using Synapse.Infrastructure.Persistence;

namespace Synapse.Api.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ProjectsController : ControllerBase
    {
        public readonly SynapseDbContext _context;

        public ProjectsController(SynapseDbContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetAll()
        {
            var projects = await _context.Projects.ToListAsync();

            return Ok(projects);
        }
        #endregion

        #region Post
        [HttpPost]
        public async Task<ActionResult<Project>> Create(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { id = project.Id }, project);
        }
        #endregion
    }
}
