using Tickets.Microservice.Constants;
using Tickets.Microservice.Options;

namespace Tickets.Microservice.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<RabbitMQCredentialsOptions>(options => configuration.GetSection(OptionsConstants.RabbitMQCredentialsSection));
    }
}
