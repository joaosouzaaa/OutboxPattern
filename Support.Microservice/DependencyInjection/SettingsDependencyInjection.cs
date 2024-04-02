using Support.Microservice.Interfaces.Settings;
using Support.Microservice.Settings.EmailSettings;
using Support.Microservice.Settings.NotificationSettings;

namespace Support.Microservice.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
