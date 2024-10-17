using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Application.Port;

public record SendEmailNotification(Guid Id, string emailAddress, string subject, string content) : ICommand
{
    public static SendEmailNotification Create(
        string emailAddress,
        string subject,
        string content)
    {
        return new SendEmailNotification(
            ICommand.GenerateId(),
            emailAddress,
            subject,
            content
        );
    }
}