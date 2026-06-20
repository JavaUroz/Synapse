using MediatR;
using Microsoft.AspNetCore.Mvc;
using Synapse.Application.Features.Projects;
using Synapse.Application.Features.Projects.Commands;
using Synapse.Application.Features.Projects.Queries;
using Synapse.Domain.Entities;
using System.Net.Http.Headers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Synapse.Api.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ProjectsController : ControllerBase
{
    public readonly IMediator _mediator;

    public ProjectsController (IMediator mediator)
    {
        _mediator = mediator;
    }

    #region Get
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProjectDto>>> GetAll()
    {
        var projects = await _mediator.Send(new GetAllProjectsQuery());

        return Ok(projects);
    }
    #endregion

    #region Post
    [HttpPost]
    public async Task<ActionResult<ProjectDto>> Create(CreateProjectCommand project)
    {
        if (string.IsNullOrEmpty(project.Name))
            return BadRequest("Project name is required.");

        var result = await _mediator.Send(project);

        return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
    }
    #endregion
}
