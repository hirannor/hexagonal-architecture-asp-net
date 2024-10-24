﻿using System.ComponentModel;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("CustomerRegistrationAndAuthFlow")]
public class CustomerRegistrationAndAuthFlowFunctionalTest :
    IClassFixture<WebApplicationFactory<Program>>,
    IClassFixture<SqlServerContainerFixture>, IDisposable
{
    private const string UsersApiBasePath = "/api/customers";
    private const string AuthApiBasePath = "/api/auth";
    private const string RegisterApiBasePath = "/api/register";

    private readonly HttpClient _client;
    private readonly WebApplicationFactory<Program> _webApplicationFactory;

    public CustomerRegistrationAndAuthFlowFunctionalTest(SqlServerContainerFixture fixture)
    {
        var clientOptions = new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        };

        _webApplicationFactory = new CustomWebApplicationFactory(fixture);
        _client = _webApplicationFactory.CreateClient(clientOptions);
    }

    [DisplayName("should display customer details after successful registration and authentication")]
    [Fact]
    public async Task DisplayCustomerAfterSuccessfulRegistrationAndAuthentication()
    {
        // given
        const string username = "user";
        const string emailAddress = "user@user.com";
        const string firstName = "John";
        const string lastName = "Doe";
        var birthOn = DateOnly.Parse("1992-02-10");
        const string password = "#TestPassword123";

        CustomerModel expectedModel = CustomerModel.From(
            username,
            emailAddress,
            firstName,
            lastName,
            birthOn,
            null
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
        HttpResponseMessage response = await _client.GetAsync($"{UsersApiBasePath}/{username}");
        CustomerModel? customerModel = await response.Content.ReadFromJsonAsync<CustomerModel>();

        // then
        customerModel.Should().BeEquivalentTo(expectedModel);
    }

    public void Dispose()
    {
        _webApplicationFactory.Dispose();
    }
}