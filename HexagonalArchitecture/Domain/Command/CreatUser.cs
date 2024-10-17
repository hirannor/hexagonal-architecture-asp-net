using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command
{
    public record CreateUser(Guid Id, string FullName, int Age) : ICommand
    {
        public static CreateUser Create(string fullName, int age)
        {
            return new CreateUser(ICommand.GenerateId(), fullName, age);
        }
    }
}