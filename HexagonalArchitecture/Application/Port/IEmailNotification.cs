namespace HexagonalArchitecture.Application.Port;

public interface IEmailNotification
{
    Task Send(SendEmailNotification cmd);
}