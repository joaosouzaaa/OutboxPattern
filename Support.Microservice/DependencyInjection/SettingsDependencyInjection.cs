using Support.Microservice.Interfaces.Setttings;
using Support.Microservice.Settings.NotificationSettings;

namespace Support.Microservice.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
