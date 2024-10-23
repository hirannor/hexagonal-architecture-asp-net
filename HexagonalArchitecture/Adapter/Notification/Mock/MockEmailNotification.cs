using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Infrastructure.Adapter;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

[Adapter(type: AdapterType.Driven)]
internal class MockEmailNotification(ILogger<MockEmailNotification> logger)
    : IEmailNotification
{
    public Task Send(SendEmailNotification cmd)
    {
        logger.LogDebug("Mock email notification adapter triggered...");

        return Task.CompletedTask;
    }
}