using Tickets.Microservice.Filters;

namespace Tickets.Microservice.DependencyInjection;

internal static class FiltersDependencyInjection
{
    internal static void AddFilterDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<NotificationFilter>();
        services.AddMvc(options => options.Filters.AddService<NotificationFilter>());
    }
}
