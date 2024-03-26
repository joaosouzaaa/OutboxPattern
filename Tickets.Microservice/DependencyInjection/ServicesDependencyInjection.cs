using Tickets.Microservice.Interfaces.Services;
using Tickets.Microservice.Services;

namespace Tickets.Microservice.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IOutboxService, OutboxService>();
        services.AddScoped<ITicketService, TicketService>();
    }
}
