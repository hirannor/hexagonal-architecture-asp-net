using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command
{
    public record CreateUser(Guid Id, string EmailAddress, string FullName, int Age) : ICommand
    {
        public static CreateUser Create(string emailAddress, string fullName, int age)
        {
            return new CreateUser(ICommand.GenerateId(), emailAddress, fullName, age);
        }
    }
}