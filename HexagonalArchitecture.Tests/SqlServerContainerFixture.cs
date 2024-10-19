using TestContainers.Container.Abstractions.Hosting;
using TestContainers.Container.Database.Hosting;
using TestContainers.Container.Database.MsSql;

namespace DotnetWebApi.Tests;

public class SqlServerContainerFixture : IAsyncLifetime
{
    private const string DatabaseName = "HexagonalArchitectureTests";
    private const string DatabaseUserName = "sa";
    private const string DatabasePassword = "YourStrong(!)Password";

    private readonly MsSqlContainer _sqlContainer = new ContainerBuilder<MsSqlContainer>()
        .ConfigureDatabaseConfiguration(DatabaseName, DatabaseUserName, DatabasePassword)
        .Build();

    public async Task InitializeAsync()
    {
        await _sqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _sqlContainer.StopAsync();
    }

    public string ConnectionString => _sqlContainer.GetConnectionString(DatabaseName);
}