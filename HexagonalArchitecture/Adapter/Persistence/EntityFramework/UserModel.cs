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
        public int Id { get; init; }

        [Required]
        [Column("USER_ID", TypeName = "varchar(200)")]
        public string UserId { get; } = userId;

        [Required]
        [Column("EMAIL_ADDRESS", TypeName = "varchar(200)")]
        public string EmailAddress { get; init; } = emailAddress;

        [Column("FULL_NAME", TypeName = "varchar(200)")]
        public string FullName { get; init; } = fullName;

        [Required] [Column("AGE")] public int Age { get; init; } = age;
    }
}