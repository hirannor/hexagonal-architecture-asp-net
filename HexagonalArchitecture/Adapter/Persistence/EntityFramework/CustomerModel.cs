using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

[Table("CUSTOMERS")]
public class CustomerModel
{
    private CustomerModel()
    {
    }

    public CustomerModel(
        string customerId,
        string username,
        string emailAddress,
        string firstName,
        string lastName,
        DateOnly birthOn,
        AddressModel? address = null)
    {
        CustomerId = customerId;
        Username = username;
        EmailAddress = emailAddress;
        FirstName = firstName;
        LastName = lastName;
        BirthOn = birthOn;
        Address = address;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; init; }

    [Required]
    [Column("CUSTOMER_ID", TypeName = "varchar(200)")]
    public string CustomerId { get; }

    [Required]
    [Column("USERNAME", TypeName = "varchar(200)")]
    public string Username { get; }

    [Required]
    [Column("EMAIL_ADDRESS", TypeName = "varchar(200)")]
    public string EmailAddress { get; set; }

    [Column("FIRST_NAME", TypeName = "varchar(200)")]
    public string FirstName { get; set; }

    [Column("LAST_NAME", TypeName = "varchar(200)")]
    public string LastName { get; set; }

    [Required] [Column("BIRTH_ON")] public DateOnly BirthOn { get; set; }

    public AddressModel? Address { get; set; }
}