using Support.Microservice.Constants;

namespace Support.Microservice.Factories;

public static class ConnectionStringFactory
{
    public static string GetConnectionString(this IConfiguration configuration)
    {
        if (Environment.GetEnvironmentVariable(EnvironmentVariables.DockerEnvironment) is EnvironmentsConstants.DockerEnvironment)
            return configuration.GetConnectionString("ContainerConnection")!;

        return configuration.GetConnectionString("LocalConnection")!;
    }
}
