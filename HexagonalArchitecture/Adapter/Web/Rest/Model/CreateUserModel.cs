using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest.Model;

public record CreateUserModel(
    [Required(ErrorMessage = "Please provide an email address.")]
    [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address.")]
    string EmailAddress,
        
    [Required(ErrorMessage = "Please provide a full name.")]
    string FullName,
        
    [Required(ErrorMessage = "Please provide age.")]
    [Range(18, 99, ErrorMessage = "Age must be between 18 and 99 years.")]
    int Age)
{
}