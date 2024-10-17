using HexagonalArchitecture.Application.Port;

namespace HexagonalArchitecture.Application.UseCase;

public interface INotificationSending
{
    void Send(SendEmailNotification cmd);
}