using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using UserModel = HexagonalArchitecture.Adapter.Web.Rest.Model.UserModel;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("UserRegistrationAndAuthFlow")]
public class UserRegistrationAndAuthFlowFunctionalTest : 
    IClassFixture<WebApplicationFactory<Program>>, 
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string UsersApiBasePath = "/api/users";
    private const string AuthApiBasePath = "/api/auth";

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;
    public UserRegistrationAndAuthFlowFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        };

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [DisplayName("should display user by id after successful authentication")]
    [Fact]
    public async Task DisplayBy_ShouldReturnUser_WhenUserExists()
    {
        // given
        const string emailAddress = "user@user.com";
        const string fullName = "Test User";
        const int age = 19;
        const string password = "#TestPassword123";
        var expectedUserModel = UserModel.From("", emailAddress, fullName, age);
        var userRegisterModel = RegisterUserModel.From(emailAddress, password, fullName, age);
        var signInUserModel = SignInUserModel.From(emailAddress, password);
        
        await _client.PostAsJsonAsync($"{AuthApiBasePath}/register", userRegisterModel);
        var authResponse = await _client.PostAsJsonAsync($"{AuthApiBasePath}/login", signInUserModel);
        var jwtToken = await authResponse.Content.ReadFromJsonAsync<JwtTokenModel>(); 

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken.value);

        // when
        var getUserByIdEmail= await _client.GetAsync($"{UsersApiBasePath}/by-email?email={emailAddress}");
        var userModel = await getUserByIdEmail.Content.ReadFromJsonAsync<UserModel>();
        
        // then
        userModel.Should().BeEquivalentTo(expectedUserModel, options => options.Excluding(user => user.UserId));
    }
    
    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }
}
