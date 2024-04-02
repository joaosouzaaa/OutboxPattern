using Support.Microservice.Interfaces.Services;
using Support.Microservice.Services;

namespace Support.Microservice.DependencyInjection;

internal static class ServicesDependencyInjection
{
    internal static void AddServicesDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<ISupportEngineerService, SupportEngineerService>();
    }
}
