using Tickets.Microservice.Data.UnitOfWork;
using Tickets.Microservice.Filters;
using Tickets.Microservice.Interfaces.Data.UnitOfWork;

namespace Tickets.Microservice.DependencyInjection;

internal static class FiltersDependencyInjection
{
    internal static void AddFilterDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<NotificationFilter>();
        services.AddMvc(options => options.Filters.AddService<NotificationFilter>());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<UnitOfWorkFilter>();

        services.AddMvc(options => options.Filters.AddService<UnitOfWorkFilter>());
    }
}
