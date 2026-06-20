using Microsoft.EntityFrameworkCore;
using Synapse.Infrastructure.Persistence;

namespace Synapse.Application.Features.Projects.Service
{
    public class ProjectService : IProjectService
    {
        public readonly SynapseDbContext _context;

        public ProjectService(SynapseDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllAsync()
        {
            var projects = await _context.Projects
                .Select(p => new ProjectDto(p.Id, p.Name, p.RepositoryUrl, p.CreatedAt))
                .ToListAsync();

            return projects;
        }

        public async Task<ProjectDto> CreateAsync(CreateProjectDto dto)
        {
            var project = new Domain.Entities.Project
            {
                Name = dto.Name,
                RepositoryUrl = dto.RepositoryUrl,
                CreatedAt = DateTime.UtcNow
            };

            _context.Projects.Add(project);
            await _context.SaveChangesAsync();

            return new ProjectDto(project.Id, project.Name, project.RepositoryUrl, project.CreatedAt);
        }
    }
}
