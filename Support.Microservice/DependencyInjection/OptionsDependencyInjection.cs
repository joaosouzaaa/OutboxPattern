using Support.Microservice.Constants;
using Support.Microservice.Options;

namespace Support.Microservice.DependencyInjection;

internal static class OptionsDependencyInjection
{
    internal static void AddOptionsDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailCredentialsOptions>(options => configuration.GetSection(OptionsConstants.EmailCredentialsSection).Bind(options));
        services.Configure<RabbitMQCredentialsOptions>(options => configuration.GetSection(OptionsConstants.RabbitMQCredentialsSection).Bind(options));
    }
}
