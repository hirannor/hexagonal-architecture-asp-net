using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Application.Port;

public record SendEmailNotification(Guid Id, string EmailAddress, string Subject, string Content) : ICommand
{
    public static SendEmailNotification Create(
        string emailAddress,
        string? subject,
        string? content)
    {
        return new SendEmailNotification(
            ICommand.GenerateId(),
            emailAddress,
            subject ?? string.Empty, 
            content ?? string.Empty   
        );
    }
}