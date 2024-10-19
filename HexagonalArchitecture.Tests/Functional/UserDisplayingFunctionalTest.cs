using System.ComponentModel;
using System.Net.Http.Json;
using FluentAssertions;
using HexagonalArchitecture.Adapter.Persistence.EntityFramework;
using HexagonalArchitecture.Adapter.Web.Rest.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using UserModel = HexagonalArchitecture.Adapter.Web.Rest.Model.UserModel;

namespace DotnetWebApi.Tests.Functional;

[DisplayName("UserDisplaying")]
public class UserDisplayingFunctionalTest : IClassFixture<WebApplicationFactory<Program>>, IClassFixture<SqlServerContainerFixture>
{
    private readonly HttpClient _client;

    public UserDisplayingFunctionalTest(WebApplicationFactory<Program> factory, SqlServerContainerFixture sqlServerContainer)
    {
        var webHostBuilder = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(UserContext));
                
                services.AddDbContext<UserContext>(options =>
                    options.UseSqlServer(sqlServerContainer.ConnectionString));
            });
        });

        _client = webHostBuilder.CreateClient();
    }

    [Fact(Skip = "This test is temporarily disabled due the issue with test containers and docker")]
    public async Task DisplayBy_ShouldReturnUser_WhenUserExists()
    {
        const string emailAddress = "user@dtest.com";
        const string fullName = "Test User";
        const int age = 19;
        
        var expectedUserModel = UserModel.From("", emailAddress, fullName, age);
        
        var userToCreate = CreateUserModel.From(emailAddress, fullName, age);
        var createdUserResponse = await _client.PostAsJsonAsync("/api/users", userToCreate);
        var createdUserModel = await createdUserResponse.Content.ReadFromJsonAsync<UserModel>();
        var userId = createdUserModel.UserId;
        
        var getUserByIdResponse = await _client.GetAsync($"/api/users/{userId}");
        var userModel = await getUserByIdResponse.Content.ReadFromJsonAsync<UserModel>();
        
        userModel.Should().BeEquivalentTo(expectedUserModel, options => options.Excluding(user => user.UserId));
    }
}
