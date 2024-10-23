namespace HexagonalArchitecture.Domain;

public class Address
{
    public Street? Street { get; private set; }
    public City? City { get; private set; }
    public PostalCode? PostalCode { get; private set; }
    public Country? Country { get; private set; }

    public static AddressBuilder Empty()
    {
        return new AddressBuilder();
    }

    public Address(Street? street, City? city, PostalCode? postalCode, Country? country)
    {
        if (street is not null)
        {
            Street = street;
        }

        if (city is not null)
        {
            City = city;
        }

        if (postalCode is not null)
        {
            PostalCode = postalCode;
        }

        if (country is not null)
        {
            Country = country;
        }
    }

    public static Address From(Street street, City city, PostalCode postalCode, Country country)
    {
        return new Address(street, city, postalCode, country);
    }
}