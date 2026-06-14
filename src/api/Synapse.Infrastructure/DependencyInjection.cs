using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Synapse.Infrastructure.Persistence;

namespace Synapse.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var provider = configuration["DatabaseProvider"]
            ?? throw new InvalidOperationException("DatabaseProvider is not setted");

        var connectionString = configuration.GetConnectionString(provider)
            ?? throw new InvalidOperationException($"There are not connection string for {provider}.");

        services.AddDbContext<SynapseDbContext>(options =>
        {
            _ = provider switch
            {
                "SqlServer" => options.UseSqlServer(connectionString),
                "PostgreSql" => options.UseOracle(connectionString),
                _ => throw new InvalidOperationException($"Unsupported database provider: {provider}")
            };
        });

        return services;
    }
}