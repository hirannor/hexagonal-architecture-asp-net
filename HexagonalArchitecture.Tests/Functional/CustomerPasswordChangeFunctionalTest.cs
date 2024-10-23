using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("CustomerPasswordChange")]
public class CustomerPasswordChangeFunctionalTest :
    IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string UsersApiBasePath = "/api/customers";
    private const string AuthApiBasePath = "/api/auth";

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public CustomerPasswordChangeFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        };

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [DisplayName("should change customer password and display customer after successful authentication")]
    [Fact]
    public async Task ChangePasswordAndDisplayCustomer()
    {
        // given
        const string username = "janedoe";
        const string emailAddress = "jane.doe@localhost.com";
        const string firstName = "Jane";
        const string lastName = "Doe";
        const string password = "#TestPassword123";
        const string newPassword = "#TestPassword123";

        DateOnly birthOn = DateOnly.Parse("1995-05-15");
        ChangePasswordModel model = ChangePasswordModel.From(password, newPassword);

        CustomerModel expectedModel = CustomerModel.From(
            username,
            emailAddress,
            firstName,
            lastName,
            birthOn,
            null
        );

        SignInModel signInModel = SignInModel.From(username, password);
        HttpResponseMessage authResponse = await _client.PostAsJsonAsync($"{AuthApiBasePath}", signInModel);
        JwtTokenModel? jwtToken = await authResponse.Content.ReadFromJsonAsync<JwtTokenModel>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken?.value);

        // when
        await _client.PutAsJsonAsync($"{UsersApiBasePath}/{username}/password", model);

        signInModel = SignInModel.From(username, password);
        authResponse = await _client.PostAsJsonAsync($"{AuthApiBasePath}", signInModel);
        jwtToken = await authResponse.Content.ReadFromJsonAsync<JwtTokenModel>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken?.value);

        HttpResponseMessage customerResponse = await _client.GetAsync($"{UsersApiBasePath}/{username}");
        CustomerModel? customerModel = await customerResponse.Content.ReadFromJsonAsync<CustomerModel>();

        // then
        customerModel.Should().BeEquivalentTo(expectedModel);
    }

    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }
}