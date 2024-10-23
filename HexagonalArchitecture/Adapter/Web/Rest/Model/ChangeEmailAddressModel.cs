using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record ChangeEmailAddressModel(
    [Required(ErrorMessage = "Please provide an email address.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    string oldEmailAddress,
    [Required(ErrorMessage = "Please provide an email address.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    string newEmailAddress)
{
    public static ChangeEmailAddressModel From(string oldEmailAddress, string newEmailAddress)
    {
        return new ChangeEmailAddressModel(oldEmailAddress, newEmailAddress);
    }
}