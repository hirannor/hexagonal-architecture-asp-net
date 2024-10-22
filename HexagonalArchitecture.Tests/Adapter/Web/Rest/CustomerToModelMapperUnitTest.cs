using System.ComponentModel;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace DotnetWebApi.Tests.Adapter.Web.Rest;

[DisplayName("CustomerToModelMapper")]
public class CustomerToModelMapperUnitTest
{
    private readonly IFunction<Customer, CustomerModel> _mapUserToModel = new CustomerToModelMapper();

    [Fact]
    [DisplayName("should map domain object to model")]
    public void TestSuccessFulMapping()
    {
        var id = CustomerId.Generate();
        const string username = "johndoe";
        const string firstName = "John";
        const string lastName = "Doe";
        const string emailAddress = "john.doe@example.com";
        var birthOn = DateOnly.Parse("1992-02-10");

        var domain = Customer.From(
            id,
            Username.From(username),
            EmailAddress.From(emailAddress),
            FirstName.From(firstName),
            LastName.From(lastName),
            DateOfBirth.From(birthOn)
        );
        var expectedModel = CustomerModel.From(username, emailAddress, firstName, lastName, birthOn);

        var result = _mapUserToModel.Apply(domain);

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