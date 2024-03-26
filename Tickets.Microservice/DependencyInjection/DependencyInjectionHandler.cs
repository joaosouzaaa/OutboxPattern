using Microsoft.EntityFrameworkCore;
using Tickets.Microservice.Constants;
using Tickets.Microservice.Data.DatabaseContexts;

namespace Tickets.Microservice.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);

            if (Environment.GetEnvironmentVariable(EnvironmentVariables.Environment) is EnvironmentsConstants.Development)
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddSettingsDependencyInjection();
        services.AddFilterDependencyInjection();
    }
}
