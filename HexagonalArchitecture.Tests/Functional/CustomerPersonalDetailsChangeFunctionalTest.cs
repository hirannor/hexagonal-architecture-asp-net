using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("CustomerPersonalDetailsChange")]
public class CustomerPersonalDetailsChangeFunctionalTest :
    IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string UsersApiBasePath = "/api/customers";
    private const string AuthApiBasePath = "/api/auth";

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public CustomerPersonalDetailsChangeFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        };

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [DisplayName("should change and display customer with new personal details")]
    [Fact]
    public async Task ChangePersonalDetailsAndDisplayCustomerWithNewPersonDetails()
    {
        // given
        const string username = "janedoe";
        const string password = "#TestPassword123";
        const string emailAddress = "jane.doe@localhost.com";
        const string firstName = "TestFirstName";
        const string lastName = "TestLastName";
        DateOnly birthOn = DateOnly.Parse("1992-01-01");
        ChangePersonalDetailsModel model = ChangePersonalDetailsModel.From(firstName, lastName, birthOn);
        CustomerModel expectedModel = CustomerModel.From(
            username,
            emailAddress,
            firstName,
            lastName,
            birthOn
        );

        SignInModel signInModel = SignInModel.From(username, password);
        HttpResponseMessage authResponse = await _client.PostAsJsonAsync($"{AuthApiBasePath}", signInModel);
        JwtTokenModel? jwtToken = await authResponse.Content.ReadFromJsonAsync<JwtTokenModel>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken?.value);

        // when
        await _client.PatchAsJsonAsync($"{UsersApiBasePath}/{username}", model);
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