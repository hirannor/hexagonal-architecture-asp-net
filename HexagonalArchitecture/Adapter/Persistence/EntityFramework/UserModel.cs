namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework
{
    public class UserModel(string id, string fullName, int age)
    {
        public string Id { get; init; } = id;
        public string FullName { get; init; } = fullName;
        public int Age { get; init; } = age;
    }
}