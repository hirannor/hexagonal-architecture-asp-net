namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework
{
    public class UserModel(string id, string emailAddress, string fullName, int age)
    {
        public string Id { get; init; } = id;

        public string EmailAddress { get; init; } = emailAddress;
        public string FullName { get; init; } = fullName;
        public int Age { get; init; } = age;
    }
}