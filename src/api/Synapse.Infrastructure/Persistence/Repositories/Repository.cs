using Microsoft.EntityFrameworkCore;
using Synapse.Application.Common.Interfaces;

namespace Synapse.Infrastructure.Persistence.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly SynapseDbContext _context;

    public Repository(SynapseDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<T>> GetAllAsync() =>
        await _context.Set<T>().ToListAsync();

    public async Task<T?> GetByIdAsync(Guid id) =>
        await _context.Set<T>().FindAsync(id);

    public async Task AddAsync(T entity) =>
        await _context.Set<T>().AddAsync(entity);
}
