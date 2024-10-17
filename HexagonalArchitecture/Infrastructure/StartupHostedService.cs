using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Infrastructure;

public class StartupHostedService(EventBusInitializer eventBusInitializer) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        eventBusInitializer.Initialize();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}