using MediatR;
using Synapse.Application.Common.Interfaces;
using Synapse.Domain.Entities;

namespace Synapse.Application.Features.Projects.Commands;

public record CreateProjectCommand(string Name, string? RepositoryUrl) : IRequest<ProjectDto>;

public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, ProjectDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateProjectCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<ProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
    {
        var project = new Project
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            RepositoryUrl = request.RepositoryUrl,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Projects.AddAsync(project);
        await _unitOfWork.SaveChangesAsync();

        return new ProjectDto(project.Id, project.Name, project.RepositoryUrl, project.CreatedAt);
    }
}
