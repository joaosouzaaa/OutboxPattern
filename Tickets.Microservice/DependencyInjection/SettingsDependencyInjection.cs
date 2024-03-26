using Tickets.Microservice.Interfaces.Setttings;
using Tickets.Microservice.Settings.NotificationSettings;

namespace Tickets.Microservice.DependencyInjection;

internal static class SettingsDependencyInjection
{
    internal static void AddSettingsDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<INotificationHandler, NotificationHandler>();
    }
}
