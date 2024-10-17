using System.ComponentModel.DataAnnotations;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Domain.Event;
using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain
{
    public class User(UserId id, string fullName, int age) : IAggregateRoot
    {
        [field: Required] public UserId Id { get; init; } = id;

        public string FullName { get; init; } = fullName;

        public int Age { get; init; } = age;

        private List<DomainEvent> _domainEvents = [];

        public static User From(UserId id, string fullName, int age)
        {
            return UserBuilder
                .Empty()
                .UserId(id)
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
                .FullName(cmd.FullName)
                .Age(cmd.Age)
                .CreateUser();

            newUser._domainEvents.Add(UserCreated.Issue(id));

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

        public User(string fullName, int age) : this(null, fullName, age)
        {
        }
    }
}