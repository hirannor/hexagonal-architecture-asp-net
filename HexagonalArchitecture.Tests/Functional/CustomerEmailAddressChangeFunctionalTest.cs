using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("CustomerEmailAddressChange")]
public class CustomerEmailAddressChangeFunctionalTest :
    IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string UsersApiBasePath = "/api/customers";
    private const string AuthApiBasePath = "/api/auth";

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public CustomerEmailAddressChangeFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        };

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [DisplayName("should change and display customer with new e-mail address")]
    [Fact]
    public async Task ChangeEmailAddressAndDisplayCustomer()
    {
        // given
        const string username = "janedoe";
        const string emailAddress = "jane.doe@localhost.com";
        const string newEmailAddress = "jane.doe_changed@localhost.com";
        const string firstName = "Jane";
        const string lastName = "Doe";
        const string password = "#TestPassword123";
        DateOnly birthOn = DateOnly.Parse("1995-05-15");
        ChangeEmailAddressModel model = ChangeEmailAddressModel.From(emailAddress, newEmailAddress);

        CustomerModel expectedModel = CustomerModel.From(
            username,
            newEmailAddress,
            firstName,
            lastName,
            birthOn
        );

        SignInModel signInModel = SignInModel.From(username, password);
        HttpResponseMessage authResponse = await _client.PostAsJsonAsync($"{AuthApiBasePath}", signInModel);
        JwtTokenModel? jwtToken = await authResponse.Content.ReadFromJsonAsync<JwtTokenModel>();

        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", jwtToken?.value);

        // when
        await _client.PutAsJsonAsync($"{UsersApiBasePath}/{username}/email-address", model);
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