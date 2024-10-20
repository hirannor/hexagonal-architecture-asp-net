using System.ComponentModel;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping;
using HexagonalArchitecture.Adapter.Web.Rest.Mapping.User;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Infrastructure;

namespace DotnetWebApi.Tests.Adapter.Web.Rest;

[DisplayName("UserToModelMapper")]
public class UserToModelMapperUnitTest
{
    private readonly IFunction<User, UserModel> _mapUserToModel = UserMappingFactory.CreateUserToModelMapper();

    [Fact]
    [DisplayName("should map User domain object to User model")]
    public void TestSuccessFulMapping()
    {
        var id = UserId.Generate();
        const string fullName = "John Doe";
        const string emailAddress = "john.doe@example.com";
        const int age = 32;

        var domain = User.From(id, EmailAddress.From(emailAddress), fullName, Age.From(age));
        var expectedModel = UserModel.From(id.Value, emailAddress, fullName, age);

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