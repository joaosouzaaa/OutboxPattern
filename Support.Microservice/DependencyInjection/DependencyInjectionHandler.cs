using Microsoft.EntityFrameworkCore;
using Support.Microservice.Constants;
using Support.Microservice.Data.DatabaseContexts;
using Support.Microservice.Factories;

namespace Support.Microservice.DependencyInjection;

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
    }
}
