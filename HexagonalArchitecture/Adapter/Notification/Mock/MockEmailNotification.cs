using HexagonalArchitecture.Application.Port;

namespace HexagonalArchitecture.Adapter.Notification.Mock;

public class MockEmailNotification(ILogger<MockEmailNotification> logger)
    : IEmailNotification
{
    private const string SendEmailNotificationCmdIsNull = "SendEmailNotification command should be not null!";

    public void Send(SendEmailNotification cmd)
    {
        if (cmd == null)
        {
            logger.LogError(SendEmailNotificationCmdIsNull);
            ArgumentNullException.ThrowIfNull(SendEmailNotificationCmdIsNull);
        }

        logger.LogDebug("Mock email notification adapter triggered...");
    }
}