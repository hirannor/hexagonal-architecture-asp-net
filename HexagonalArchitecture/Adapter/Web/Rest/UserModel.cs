namespace HexagonalArchitecture.Adapter.Web.Rest
{
    public record UserModel(string Id, string EmailAddress, string FullName, int Age)
    {
        public static UserModel From(string id, string emailAddress, string fullName, int age)
        {
            return new UserModel(id, emailAddress, fullName, age);
        }
    }
}