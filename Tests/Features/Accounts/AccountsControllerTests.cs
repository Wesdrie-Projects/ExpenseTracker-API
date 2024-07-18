using API.Features.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Security.Claims;
using Tests.Fixtures;

namespace Tests.Features.Accounts;

public class AccountsControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture _fixture;

    public AccountsControllerTests(DatabaseFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task CreateAsync_CreatesAccount_ReturnsOk()
    {
        // Arrange
        var user = _fixture.Context.Users.FirstOrDefault();
        var userClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id)
        };
        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(context => context.User).Returns(userPrincipal);

        var controller = new AccountsController(_fixture.Context)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            }
        };

        var request = new CreateAccountRequest
        { 
            Name = "Test Account"
        };

        var previousCount = _fixture.Context.Accounts.Count();

        // Act
        var response = await controller.CreateAsync(request);

        // Assert
        var updateCount = _fixture.Context.Accounts.Count();

        Assert.IsType<OkObjectResult>(response);
        Assert.Equal(previousCount + 1, updateCount);
    }

    [Fact]
    public async Task CreateAsync_UserNotFound_ThrowsException()
    {
        // Arrange
        var nonExistentUserId = "non-existent-user-id";
        var userClaims = new List<Claim>
    {
        new(ClaimTypes.NameIdentifier, nonExistentUserId)
    };
        var userPrincipal = new ClaimsPrincipal(new ClaimsIdentity(userClaims));

        var mockHttpContext = new Mock<HttpContext>();
        mockHttpContext.Setup(context => context.User).Returns(userPrincipal);

        var controller = new AccountsController(_fixture.Context)
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = mockHttpContext.Object
            }
        };

        var request = new CreateAccountRequest
        {
            Name = "Test Account"
        };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(async () =>
            await controller.CreateAsync(request));
        Assert.Contains($"User Not Found:", exception.Message);
    }

}
