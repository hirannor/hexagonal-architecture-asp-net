using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command
{
    public record CreateUser(Guid Id, EmailAddress EmailAddress, string FullName, Age Age) : ICommand
    {
        public static CreateUser Issue(EmailAddress emailAddress, string fullName, Age age)
        {
            return new CreateUser(ICommand.GenerateId(), emailAddress, fullName, age);
        }
    }
}