using Support.Microservice.Interfaces.Mappers;
using Support.Microservice.Mappers;

namespace Support.Microservice.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ISupportEngineerMapper, SupportEngineerMapper>();
    }
}
