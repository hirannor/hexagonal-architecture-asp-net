using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record RegisterUserModel(
    [Required(ErrorMessage = "Please provide an email address.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    string EmailAddress,

    [Required(ErrorMessage = "Please provide a password.")]
    string Password,
        
    [Required(ErrorMessage = "Please provide a full name.")]
    string FullName,
        
    [Required(ErrorMessage = "Please provide age.")]
    [Range(18, 99, ErrorMessage = "Age must be between 18 and 99 years.")]
    int Age)
{
    public static RegisterUserModel From(string emailAddress, string password, string fullName, int age)
    {
        return new RegisterUserModel(emailAddress, password, fullName, age);
    }
}