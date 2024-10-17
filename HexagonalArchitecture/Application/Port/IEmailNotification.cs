namespace HexagonalArchitecture.Application.Port;

public interface IEmailNotification
{
    void Send(SendEmailNotification cmd);
}