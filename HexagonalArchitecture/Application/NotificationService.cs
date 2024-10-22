using HexagonalArchitecture.Application.Port;
using HexagonalArchitecture.Application.UseCase;

namespace HexagonalArchitecture.Application;

internal class NotificationService(ILogger<NotificationService> logger, IEmailNotification notification)
    : INotificationSending
{
    public void Send(SendEmailNotification cmd)
    {
        logger.LogInformation("Sending out email notification for {emailAddress}...", cmd.EmailAddress);
        notification.Send(cmd);
        logger.LogInformation("Email notification send was successful...");
    }
}