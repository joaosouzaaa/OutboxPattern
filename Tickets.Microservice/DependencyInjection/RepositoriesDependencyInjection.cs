using Tickets.Microservice.Data.Repositories;
using Tickets.Microservice.Interfaces.Repositories;

namespace Tickets.Microservice.DependencyInjection;

internal static class RepositoriesDependencyInjection
{
    internal static void AddRepositoriesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IOutboxRepository, OutboxRepository>();
        services.AddScoped<ITicketRepository, TicketRepository>();
    }
}
