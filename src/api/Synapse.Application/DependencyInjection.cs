
using Microsoft.Extensions.DependencyInjection;
using Synapse.Application.Features.Projects;
using System.Reflection;

namespace Synapse.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
