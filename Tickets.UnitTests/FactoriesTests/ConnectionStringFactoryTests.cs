using Microsoft.Extensions.Configuration;
using Tickets.Microservice.Constants;
using Tickets.Microservice.Factories;

namespace Tickets.UnitTests.FactoriesTests;
public sealed class ConnectionStringFactoryTests
{
    [Fact]
    public void GetConnectionString_ReturnsContainerConnection()
    {
        // A
        const string containerConnection = "test";
        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"ConnectionStrings:ContainerConnection", containerConnection }
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();

        Environment.SetEnvironmentVariable(EnvironmentVariables.DockerEnvironment, EnvironmentsConstants.DockerEnvironment);

        // A
        var connectionStringResult = configuration.GetConnectionString();

        // A
        Assert.Equal(containerConnection, connectionStringResult);
    }

    [Fact]
    public void GetConnectionString_ReturnsLocalConnection()
    {
        // A
        const string localConnection = "joao";
        var inMemoryCollection = new Dictionary<string, string>()
        {
            {"ConnectionStrings:LocalConnection", localConnection}
        };
        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemoryCollection!)
            .Build();

        Environment.SetEnvironmentVariable(EnvironmentVariables.DockerEnvironment, "any value");

        // A
        var connectionStringResult = configuration.GetConnectionString();

        // A
        Assert.Equal(localConnection, connectionStringResult);
    }
}
