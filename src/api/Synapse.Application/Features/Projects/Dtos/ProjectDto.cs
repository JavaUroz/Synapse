namespace Synapse.Application.Features.Projects;

public record ProjectDto(Guid Id, string Name, string? RepositoryUrl, DateTime CreatedAt);