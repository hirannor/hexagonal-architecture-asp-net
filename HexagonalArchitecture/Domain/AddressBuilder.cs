namespace HexagonalArchitecture.Domain;

public class AddressBuilder
{
    private Street? _street;
    private City? _city;
    private PostalCode? _postalCode;
    private Country? _country;

    public AddressBuilder WithStreet(Street? street)
    {
        _street = street;
        return this;
    }

    public AddressBuilder WithCity(City? city)
    {
        _city = city;
        return this;
    }

    public AddressBuilder WithPostalCode(PostalCode? postalCode)
    {
        _postalCode = postalCode;
        return this;
    }

    public AddressBuilder WithCountry(Country? country)
    {
        _country = country;
        return this;
    }

    public Address Build()
    {
        return new Address(_street, _city, _postalCode, _country);
    }
}