using Testcontainers.MsSql;

namespace DotnetWebApi.Tests;

public class SqlServerContainerFixture : IAsyncLifetime
{

    private readonly MsSqlContainer _msSqlContainer = new MsSqlBuilder().Build();

    public async Task InitializeAsync()
    {
        await _msSqlContainer.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await _msSqlContainer.StopAsync();
    }

    public string ConnectionString => _msSqlContainer.GetConnectionString();
}