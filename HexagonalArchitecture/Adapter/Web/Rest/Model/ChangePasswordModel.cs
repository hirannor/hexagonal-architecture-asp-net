using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangePasswordModel(
    [Required(ErrorMessage = "Please provide a password.")]
    [Length(6, 20, ErrorMessage = "Please provide a password between 6 and 20 characters.")]
    string OldPassword,
    [Required(ErrorMessage = "Please provide a password.")]
    [Length(6, 20, ErrorMessage = "Please provide a password between 6 and 20 characters.")]
    string NewPassword)
{
    public static ChangePasswordModel From(string oldPassword, string newPassword)
    {
        return new ChangePasswordModel(oldPassword, newPassword);
    }
}