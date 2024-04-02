using Support.Microservice.Constants;
using Support.Microservice.Options;

namespace Support.Microservice.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailCredentialsOptions>(configuration.GetSection(OptionsConstants.EmailCredentialsSection));
        services.Configure<RabbitMQCredentialsOptions>(configuration.GetSection(OptionsConstants.RabbitMQCredentialsSection));
    }
}
