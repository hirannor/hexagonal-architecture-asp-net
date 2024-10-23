using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HexagonalArchitecture.Adapter.Persistence.EntityFramework;

[Owned]
public class AddressModel
{
    [Column("STREET_NAME", TypeName = "varchar(200)")]
    public string StreetName { get; private set; }

    [Column("STREET_NUMBER", TypeName = "varchar(20)")]
    public string StreetNumber { get; private set; }

    [Column("CITY_NAME", TypeName = "varchar(200)")]
    public string CityName { get; private set; }

    [Column("POSTAL_CODE", TypeName = "varchar(20)")]
    public string PostalCode { get; private set; }

    [Column("COUNTRY_NAME", TypeName = "varchar(200)")]
    public string CountryName { get; private set; }

    public AddressModel()
    {
    }

    private AddressModel(string streetName, string streetNumber, string cityName, string postalCode, string countryName)
    {
        StreetName = streetName;
        StreetNumber = streetNumber;
        CityName = cityName;
        PostalCode = postalCode;
        CountryName = countryName;
    }

    public static AddressModel From(string streetName, string streetNumber, string cityName, string postalCode,
        string countryName)
    {
        return new AddressModel(streetName, streetNumber, cityName, postalCode, countryName);
    }
}