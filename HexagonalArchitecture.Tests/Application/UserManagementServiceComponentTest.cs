using System.ComponentModel;
using FluentAssertions;
using HexagonalArchitecture.Application;
using HexagonalArchitecture.Application.UseCase;
using HexagonalArchitecture.Domain;
using HexagonalArchitecture.Domain.Command;
using HexagonalArchitecture.Infrastructure.Messaging;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace DotnetWebApi.Tests.Application;

[DisplayName("UserManagementService")]
public class UserManagementServiceComponentTest
{
    private readonly Mock<IUserRepository> _repository;
    private readonly Mock<IEventPublishing> _publishing;
    private readonly UserManagementService _userManagementService;
    private readonly ILogger<UserManagementService> _logger;

    public UserManagementServiceComponentTest()
    {
        _logger = new NullLogger<UserManagementService>();
        _repository = new Mock<IUserRepository>();
        _publishing = new Mock<IEventPublishing>();
        _userManagementService = new UserManagementService(_repository.Object, _logger, _publishing.Object);
    }

    [Fact]
    [DisplayName("should create and store a new user")]
    public async void TestCreateUser()
    {
        const string emailAddress = "john.doe@example.com";
        const string fullName = "John Doe";
        const int age = 32;
        var cmd = CreateUser.Issue(EmailAddress.From(emailAddress), fullName, Age.From(age));
        var expected = User.From(UserId.Generate(), EmailAddress.From(emailAddress), fullName, Age.From(age));

        _repository.Setup(users => users.Insert(It.IsAny<User>()))
            .Returns(Task.CompletedTask);

        _publishing.Setup(events => events.Publish(It.IsAny<IEnumerable<Message>>()));
        var result = await _userManagementService.CreateBy(cmd);

        result.Should().NotBeNull();
        result.Should().BeEquivalentTo(expected, options => options.Excluding(user => user.UserId));

        // verify mocks
    }
}