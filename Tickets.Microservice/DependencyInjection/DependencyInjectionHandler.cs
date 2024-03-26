using Microsoft.EntityFrameworkCore;
using Tickets.Microservice.Constants;
using Tickets.Microservice.Data.DatabaseContexts;
using Tickets.Microservice.Factories;

namespace Tickets.Microservice.DependencyInjection;

internal static class DependencyInjectionHandler
{
    internal static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCorsDependencyInjection();

        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString();
            options.UseSqlServer(connectionString);

            if (Environment.GetEnvironmentVariable(EnvironmentVariables.Environment) is EnvironmentsConstants.Development)
            {
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        });

        services.AddSettingsDependencyInjection();
        services.AddFilterDependencyInjection();
        services.AddRepositoriesDependencyInjection();
        services.AddMappersDependencyInjection();
        services.AddValidatorsDependencyInjection();
        services.AddServicesDependencyInjection();
        services.AddOptionsDependencyInjection(configuration);
        services.AddBackgroundServicesDependencyInjection();
        services.AddPublishersDependencyInjection();
    }
}
