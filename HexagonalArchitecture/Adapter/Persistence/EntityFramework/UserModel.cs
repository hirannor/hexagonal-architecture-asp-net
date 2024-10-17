using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework
{
    [Table("USERS")]
    public class UserModel(string userId, string emailAddress, string fullName, int age)
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("ID")]
        public string Id { get; private set; }

        [Required]
        [Column("USER_ID", TypeName = "varchar(200)")]
        public string UserId { get; } = userId;

        [Required]
        [Column("EMAIL_ADDRESS", TypeName = "varchar(200)")]
        public string EmailAddress { get; init; } = emailAddress;

        [Column("FULL_NAME", TypeName = "varchar(200)")]
        public string FullName { get; init; } = fullName;

        [Required]
        [Column("AGE")]
        [Range(18, 100, ErrorMessage = "Age must be between 18 and 100")]
        public int Age { get; init; } = age;
    }
}