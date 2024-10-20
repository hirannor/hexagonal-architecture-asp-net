using System.ComponentModel;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using UserModel = HexagonalArchitecture.Adapter.Web.Rest.Model.UserModel;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("UserDisplaying")]
public class UserDisplayingFunctionalTest : 
    IClassFixture<WebApplicationFactory<Program>>, 
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string ApiBasePath = "/api/users/";

    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    private readonly HttpClient _client;

    public UserDisplayingFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions();
        clientOptions.AllowAutoRedirect = false;

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [Fact(Skip = "Test should be adjusted due the newly introduced authentication process")]
    [DisplayName("should display user by id after successful creation")]
    public async Task DisplayBy_ShouldReturnUser_WhenUserExists()
    {
        const string emailAddress = "user@user.com";
        const string fullName = "Test User";
        const int age = 19;
        
        var expectedUserModel = UserModel.From("", emailAddress, fullName, age);
        
        var userToCreate = CreateUserModel.From(emailAddress, fullName, age);
        var createdUserResponse = await _client.PostAsJsonAsync(ApiBasePath, userToCreate);
        var createdUserModel = await createdUserResponse.Content.ReadFromJsonAsync<UserModel>();
        var userId = createdUserModel.UserId;
        
        var getUserByIdResponse = await _client.GetAsync($"{ApiBasePath}{userId}");
        var userModel = await getUserByIdResponse.Content.ReadFromJsonAsync<UserModel>();
        
        userModel.Should().BeEquivalentTo(expectedUserModel, options => options.Excluding(user => user.UserId));
    }
    
    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }
}
