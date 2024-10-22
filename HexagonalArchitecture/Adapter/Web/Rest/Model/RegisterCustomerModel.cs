using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record RegisterCustomerModel(
    [Required(ErrorMessage = "Please provide a username.")]
    string Username,
    [Required(ErrorMessage = "Please provide an email address.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    string EmailAddress,
    [Required(ErrorMessage = "Please provide a password.")]
    [Length(6, 20, ErrorMessage = "Please provide a password between 6 and 20 characters.")]
    string Password,
    [Required(ErrorMessage = "Please provide a first name.")]
    string FirstName,
    [Required(ErrorMessage = "Please provide a last name.")]
    string LastName,
    [Required(ErrorMessage = "Please provide birth date.")]
    DateOnly BirthOn)
{
    public static RegisterCustomerModel From(
        string username,
        string emailAddress,
        string password,
        string firstName,
        string lastName,
        DateOnly birthOn)
    {
        return new RegisterCustomerModel(
            username,
            emailAddress,
            password,
            firstName,
            lastName,
            birthOn
        );
    }
}