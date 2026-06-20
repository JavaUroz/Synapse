using Synapse.Domain.Entities;

namespace Synapse.Application.Common.Interfaces;

public interface IUnitOfWork
{
    IRepository<Project> Projects { get; }
    Task<int> SaveChangesAsync();
}
