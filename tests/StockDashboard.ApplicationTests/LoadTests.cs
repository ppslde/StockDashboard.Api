using FluentAssertions;
using NSubstitute;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Features.Exchanges;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace StockDashboard.ApplicationTests {
  public class LoadTests {

    [Fact]
    public async Task Load_Should_Succeed_WithZeroExchanges() {

      // Arrange
      var stocks = Substitute.For<IStockService>();
      var exRep = Substitute.For<IExchangeRepository>();

      stocks.GetExchanges().Returns(Task.FromResult(Enumerable.Empty<Exchange>()));

      // Act
      var uut = await new Load.CommandHandler(stocks, exRep).Handle(new Load.Command { }, CancellationToken.None);

      // Assert
      uut.Count().Should().Be(0);
    }
  }
}
