using Synapse.Application.Common.Interfaces;
using Synapse.Domain.Entities;
using Synapse.Infrastructure.Persistence.Repositories;
namespace Synapse.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly SynapseDbContext _context;

    public IRepository<Project> Projects { get; }

    public UnitOfWork(SynapseDbContext context)
    {
        _context = context;
        Projects = new Repository<Project>(_context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
