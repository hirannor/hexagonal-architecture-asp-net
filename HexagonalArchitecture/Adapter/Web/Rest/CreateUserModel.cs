using System.ComponentModel.DataAnnotations;

namespace HexagonalArchitecture.Adapter.Web.Rest
{
    public record CreateUserModel(
        [Required(ErrorMessage = "Please provide Full Name")]
        string FullName,
        [Required(ErrorMessage = "Please provide Age")]
        int Age)
    {
    }
}