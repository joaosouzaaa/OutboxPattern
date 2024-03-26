using Tickets.Microservice.BackgroundServices;

namespace Tickets.Microservice.DependencyInjection;

internal static class BackgroundServicesDependencyInjection
{
    internal static void AddBackgroundServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddHostedService<OutboxBackgroundService>();
    }
}
