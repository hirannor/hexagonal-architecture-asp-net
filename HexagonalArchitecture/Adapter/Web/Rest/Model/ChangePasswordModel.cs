namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangePasswordModel(string OldPassword, string NewPassword)
{
    public static ChangePasswordModel From(string oldPassword, string newPassword)
    {
        return new ChangePasswordModel(oldPassword, newPassword);
    }
}