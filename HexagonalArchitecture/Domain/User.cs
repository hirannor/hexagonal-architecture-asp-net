using System.ComponentModel.DataAnnotations;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain
{
    public class User(UserId id, string emailAddress, string fullName, int age) : IAggregateRoot
    {
        [field: Required] public UserId Id { get; init; } = id;

        [field: Required] public string EmailAddress { get; } = emailAddress;

        [field: Required] public string FullName { get; init; } = fullName;

        [field: Required] public int Age { get; init; } = age;

        private readonly List<DomainEvent> _domainEvents = [];

        public static User From(UserId id, string emailAddress, string fullName, int age)
        {
            return UserBuilder
                .Empty()
                .UserId(id)
                .EmailAddress(emailAddress)
                .FullName(fullName)
                .Age(age)
                .CreateUser();
        }

        public static User Create(CreateUser cmd)
        {
            if (cmd == null)
            {
                ArgumentNullException.ThrowIfNull("CreateUser command cannot be null!");
            }

            var id = UserId.Generate();

            var newUser = UserBuilder
                .Empty()
                .UserId(id)
                .EmailAddress(cmd.EmailAddress)
                .FullName(cmd.FullName)
                .Age(cmd.Age)
                .CreateUser();

            newUser._domainEvents.Add(UserCreated.Issue(id, cmd.EmailAddress));

            return newUser;
        }

        public void ClearEvents()
        {
            _domainEvents.Clear();
        }

        public List<DomainEvent> ListEvents()
        {
            return [.._domainEvents];
        }
    }
}