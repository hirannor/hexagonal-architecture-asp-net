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
    private const string RegisterApiBasePath = "/api/register";

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
        ChangePersonalDetailsModel model = ChangePersonalDetailsModel.From(firstName, lastName, birthOn, null);
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
        await _client.PatchAsJsonAsync($"{UsersApiBasePath}/{username}", model);
        HttpResponseMessage customerResponse = await _client.GetAsync($"{UsersApiBasePath}/{username}");

        CustomerModel? customerModel = await customerResponse.Content.ReadFromJsonAsync<CustomerModel>();

        // then
        customerModel.Should().BeEquivalentTo(expectedModel);
    }


    [DisplayName("should add address details and display after successful registration")]
    [Fact]
    public async Task ChangePersonalDetailsAndDisplayCustomerWithNewAddressDetails()
    {
        // given
        const string username = "user";
        const string emailAddress = "user@user.com";
        const string firstName = "John";
        const string lastName = "Doe";
        var birthOn = DateOnly.Parse("1992-02-10");
        const string password = "#TestPassword123";
        const string street = "Main Street";
        const string streetNumber = "13";
        const string postalCode = "12345";
        const string city = "City";
        const string country = "United States";
        AddressModel address = AddressModel.From(
            StreetModel.From(street, streetNumber),
            postalCode,
            city,
            country
        );
        ChangePersonalDetailsModel model = ChangePersonalDetailsModel.From(firstName, lastName, birthOn, address);

        CustomerModel expectedModel = CustomerModel.From(
            username,
            emailAddress,
            firstName,
            lastName,
            birthOn,
            address
        );

        RegisterCustomerModel registerModel = RegisterCustomerModel.From(
            username,
            emailAddress,
            password,
            firstName,
            lastName,
            birthOn
        );

        SignInModel signInModel = SignInModel.From(username, password);

        await _client.PostAsJsonAsync($"{RegisterApiBasePath}", registerModel);
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