using Support.Microservice.Consumers;

namespace Support.Microservice.DependencyInjection;

internal static class ConsumersDependencyInjection
{
    internal static void AddConsumersDependencyInjection(this IServiceCollection services)
    {
        services.AddHostedService<TicketCreatedConsumer>();
    }
}
