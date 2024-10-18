using System.ComponentModel.DataAnnotations;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure;
using HexagonalArchitecture.Infrastructure.Eventing;

namespace HexagonalArchitecture.Domain
{
    public class User(UserId userId, EmailAddress emailAddress, string fullName, Age age) : IAggregateRoot
    {
        [field: Required] public UserId UserId { get; } = userId;

        [field: Required] public EmailAddress EmailAddress { get; set; } = emailAddress;

        [field: Required] public string FullName { get; set; } = fullName;

        [field: Required] public Age Age { get; set; } = age;

        private readonly List<DomainEvent> _domainEvents = [];

        public static User From(UserId id, EmailAddress emailAddress, string fullName, Age age)
        {
            return UserBuilder
                .Empty()
                .UserId(id)
                .EmailAddress(emailAddress)
                .FullName(fullName)
                .Age(age)
                .CreateUser();
        }

        public User ChangeBy(ChangeUserDetails cmd)
        {
            EmailAddress = cmd.EmailAddress;
            FullName = cmd.FullName;
            Age = cmd.Age;

            _domainEvents.Add(UserDetailsChanged.Issue(cmd.EmailAddress, cmd.FullName, cmd.Age));

            return this;
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