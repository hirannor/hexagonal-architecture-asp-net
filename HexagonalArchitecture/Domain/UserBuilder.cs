namespace HexagonalArchitecture.Domain
{
    public class UserBuilder
    {
        private UserId _id;
        private string _emailAddress;
        private string _fullName;
        private int _age;

        public static UserBuilder Empty()
        {
            return new UserBuilder();
        }

        public UserBuilder UserId(UserId userId)
        {
            _id = userId;
            return this;
        }

        public UserBuilder EmailAddress(string emailAddress)
        {
            _emailAddress = emailAddress;
            return this;
        }

        public UserBuilder FullName(string fullName)
        {
            _fullName = fullName;
            return this;
        }

        public UserBuilder Age(int age)
        {
            _age = age;
            return this;
        }

        public User CreateUser()
        {
            return new User(_id, _emailAddress, _fullName, _age);
        }
    }
}