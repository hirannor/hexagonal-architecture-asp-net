﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

[Table("CUSTOMERS")]
public class CustomerModel(
    string customerId,
    string username,
    string emailAddress,
    string firstName,
    string lastName,
    DateOnly birthOn)
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ID")]
    public int Id { get; init; }

    [Required]
    [Column("CUSTOMER_ID", TypeName = "varchar(200)")]
    public string CustomerId { get; } = customerId;

    [Required]
    [Column("USERNAME", TypeName = "varchar(200)")]
    public string Username { get; } = username;

    [Required]
    [Column("EMAIL_ADDRESS", TypeName = "varchar(200)")]
    public string EmailAddress { get; set; } = emailAddress;

    [Column("FIRST_NAME", TypeName = "varchar(200)")]
    public string FirstName { get; set; } = firstName;

    [Column("LAST_NAME", TypeName = "varchar(200)")]
    public string LastName { get; set; } = lastName;

    [Required] [Column("BIRTH_ON")] public DateOnly BirthOn { get; set; } = birthOn;
}