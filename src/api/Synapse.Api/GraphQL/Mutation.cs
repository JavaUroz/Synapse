using MediatR;
using Synapse.Application.Features.Projects;
using Synapse.Application.Features.Projects.Commands;

namespace Synapse.Api.GraphQL;

public class Mutation
{
    public async Task<ProjectDto> CreateProject(CreateProjectCommand input, [Service] IMediator mediator)
    {
        await mediator.Send(input);

        var project = new ProjectDto(Guid.NewGuid(), input.Name, input.RepositoryUrl, DateTime.UtcNow);

        return project;
    }
}
