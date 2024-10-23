using HexagonalArchitecture.Infrastructure;

namespace HexagonalArchitecture.Domain.Command;

public record ChangePersonalDetails : ICommand
{
    public Guid Id { get; }
    public string Username { get; }
    public string? FirstName { get; }
    public string? LastName { get; }
    public DateOnly? BirthOn { get; }
    public string? StreetName { get; }
    public string? StreetNumber { get; }
    public string? City { get; }
    public string? PostalCode { get; }
    public string? Country { get; }

    private ChangePersonalDetails(Builder builder)
    {
        Id = builder.Id;
        Username = builder.Username;
        FirstName = builder.FirstName;
        LastName = builder.LastName;
        BirthOn = builder.BirthOn;
        StreetName = builder.StreetName;
        StreetNumber = builder.StreetNumber;
        PostalCode = builder.PostalCode;
        City = builder.City;
        Country = builder.Country;
    }

    public static Builder Empty(string username)
    {
        return new Builder(username);
    }

    public class Builder(string username)
    {
        public Guid Id { get; private set; } = ICommand.GenerateId();
        public string Username { get; private set; } = username;
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public DateOnly? BirthOn { get; private set; }
        public string? StreetName { get; private set; }
        public string? StreetNumber { get; private set; }
        public string? City { get; private set; }
        public string? PostalCode { get; private set; }
        public string? Country { get; private set; }

        public Builder WithFirstName(string? firstName)
        {
            FirstName = firstName;
            return this;
        }

        public Builder WithLastName(string? lastName)
        {
            LastName = lastName;
            return this;
        }

        public Builder WithBirthOn(DateOnly? birthOn)
        {
            BirthOn = birthOn;
            return this;
        }

        public Builder WithStreetName(string? streetName)
        {
            StreetName = streetName;
            return this;
        }

        public Builder WithStreetNumber(string? streetNumber)
        {
            StreetNumber = streetNumber;
            return this;
        }

        public Builder WithPostalCode(string? postalCode)
        {
            PostalCode = postalCode;
            return this;
        }

        public Builder WithCity(string? city)
        {
            City = city;
            return this;
        }

        public Builder WithCountry(string? country)
        {
            Country = country;
            return this;
        }

        public ChangePersonalDetails Issue()
        {
            return new ChangePersonalDetails(this);
        }
    }
}