namespace Synapse.Application.Features.Projects;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync();
    Task<ProjectDto> CreateAsync(CreateProjectDto dto);
}