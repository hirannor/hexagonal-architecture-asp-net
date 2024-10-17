namespace HexagonalArchitecture.Adapter.Web.Rest
{
    public record UserModel(string Id, string FullName, int Age)
    {
        public static UserModel From(string id, string fullName, int age)
        {
            return new UserModel(id, fullName, age);
        }
    }
}