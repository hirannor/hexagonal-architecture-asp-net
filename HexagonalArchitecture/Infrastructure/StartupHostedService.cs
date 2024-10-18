using HexagonalArchitecture.Infrastructure.Database;
using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Infrastructure;

public class StartupHostedService(
    EventBusInitializer eventBusInitializer,
    IServiceProvider serviceProvider)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var dbMigrator = scope.ServiceProvider.GetRequiredService<IDatabaseMigrator>();
            await dbMigrator.MigrateAsync(cancellationToken);
        }

        eventBusInitializer.Initialize();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}