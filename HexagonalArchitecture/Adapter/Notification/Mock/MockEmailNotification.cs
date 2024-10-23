using HexagonalArchitecture.Application.Port;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

internal class MockEmailNotification(ILogger<MockEmailNotification> logger)
    : IEmailNotification
{
    public Task Send(SendEmailNotification cmd)
    {
        logger.LogDebug("Mock email notification adapter triggered...");

        return Task.CompletedTask;
    }
}