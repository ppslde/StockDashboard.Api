using FluentAssertions;
using NSubstitute;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Features.Authorization;
using StockDashboard.Application.Interfaces.Authentication;
using StockDashboard.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StockDashboard.ApplicationTests {
  public class AuthorizeTests {

    [Fact]
    public async Task Authorize_Should_Succeed() {

      // Arrange
      var auth = Substitute.For<IAuthenticationService>();
      var userRep = Substitute.For<IUserRepository>();

      auth.GetUserToken(Arg.Any<string>()).Returns("MYTESTTOKEN");
      userRep.GetUserByNameAndPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult(new Application.Entities.AppUser { Name = "TESTUSER" }));

      // Act
      var uut = await new Authorize.CommandHandler(auth, userRep).Handle(new Authorize.Command { }, CancellationToken.None);

      // Assert
      uut.Should().Be("MYTESTTOKEN");
    }

    [Fact]
    public async Task Authorize_Should_Fail() {

      // Arrange
      var auth = Substitute.For<IAuthenticationService>();
      var userRep = Substitute.For<IUserRepository>();

      auth.GetUserToken(Arg.Any<string>()).Returns("MYTESTTOKEN");
      userRep.GetUserByNameAndPassword(Arg.Any<string>(), Arg.Any<string>()).Returns(Task.FromResult<AppUser>(default));

      // Act
      var uut = await new Authorize.CommandHandler(auth, userRep).Handle(new Authorize.Command { }, CancellationToken.None);

      // Assert
      uut.Should().Be(null);
    }
  }
}
