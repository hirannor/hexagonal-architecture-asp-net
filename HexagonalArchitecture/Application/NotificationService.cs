using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;

namespace HexagonalArchitecture.Application;

public class NotificationService(ILogger<NotificationService> logger, IEmailNotification notification)
    : INotificationSending
{
    private const string SendEmailNotificationCmdIsNull = "SendEmailNotification command should be not null!";

    public void Send(SendEmailNotification cmd)
    {
        if (cmd == null)
        {
            logger.LogError(SendEmailNotificationCmdIsNull);
            ArgumentNullException.ThrowIfNull(SendEmailNotificationCmdIsNull);
        }

        logger.LogInformation("Sending out email notification for {emailAddress}...", cmd.emailAddress);
        notification.Send(cmd);
        logger.LogInformation("Email notification send was successful...");
    }
}