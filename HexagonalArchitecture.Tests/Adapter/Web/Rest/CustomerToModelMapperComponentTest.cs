using System.ComponentModel;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace DotnetWebApi.Tests.Adapter.Web.Rest;

[DisplayName("CustomerToModelMapper")]
public class CustomerToModelMapperComponentTest
{
    private readonly IFunction<Customer, CustomerModel> _mapUserToModel = new CustomerToModelMapper();

    [Fact]
    [DisplayName("should map domain object to model")]
    public void TestSuccessFulMapping()
    {
        // given
        CustomerId id = CustomerId.Generate();
        const string username = "johndoe";
        const string firstName = "John";
        const string lastName = "Doe";
        const string emailAddress = "john.doe@example.com";
        DateOnly birthOn = DateOnly.Parse("1992-02-10");
        const string streetName = "main st";
        const string streetNumber = "123";
        const string cityName = "new york";
        const string postalCodeValue = "10001";
        const string countryName = "united states";

        Customer domain = Customer.From(
            id,
            Username.From(username),
            EmailAddress.From(emailAddress),
            FirstName.From(firstName),
            LastName.From(lastName),
            DateOfBirth.From(birthOn),
            Address.From(
                Street.From(streetName, streetNumber),
                City.From(cityName),
                PostalCode.From(postalCodeValue),
                Country.From(countryName)
            )
        );
        AddressModel expectedAddress = AddressModel.From(
            StreetModel.From(streetName, streetNumber),
            postalCodeValue,
            cityName,
            countryName
        );
        CustomerModel expectedModel =
            CustomerModel.From(username, emailAddress, firstName, lastName, birthOn, expectedAddress);

        // when
        CustomerModel result = _mapUserToModel.Apply(domain);

        // then
        result.Should().BeEquivalentTo(expectedModel);
    }

    [Fact]
    [DisplayName("should map null to literal null")]
    public void TestNullMapping()
    {
        var model = _mapUserToModel.Apply(null);
        model.Should().BeNull("model should not be null");
    }
}