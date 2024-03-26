using Tickets.Microservice.Interfaces.Mappers;
using Tickets.Microservice.Mappers;

namespace Tickets.Microservice.DependencyInjection;

internal static class MappersDependencyInjection
{
    internal static void AddMappersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<ITicketMapper, TicketMapper>();
    }
}
