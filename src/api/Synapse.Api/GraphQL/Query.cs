using Synapse.Domain.Entities;
using Synapse.Infrastructure.Persistence;

namespace Synapse.Api.GraphQL;

public class Query
{
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    public IQueryable<Project> GetProjects(SynapseDbContext context) => context.Projects;
}
