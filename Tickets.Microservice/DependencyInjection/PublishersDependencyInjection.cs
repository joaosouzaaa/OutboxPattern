using Tickets.Microservice.Interfaces.Publishers;
using Tickets.Microservice.Publishers;

namespace Tickets.Microservice.DependencyInjection;

internal static class PublishersDependencyInjection
{
    internal static void AddPublishersDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IOutboxPublisher, OutboxPublisher>();
    }
}
