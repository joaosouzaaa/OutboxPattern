using Support.Microservice.Data.Repositories;
using Support.Microservice.Interfaces.Repositories;

namespace Support.Microservice.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ISupportEngineerRepository, SupportEngineerRepository>();
    }
}
