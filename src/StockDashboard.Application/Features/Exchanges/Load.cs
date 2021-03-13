using MediatR;
using StockDashboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Load {
    public class Command: IRequest {
    }

    public class CommandHandler : IRequestHandler<Command> {
      private readonly IStockService _stocks;

      public CommandHandler(IStockService stocks) {
        _stocks = stocks;
      }

      public async Task<Unit> Handle(Command request, CancellationToken cancellationToken) {

        var exchanges = await _stocks.GetExchanges();

        return Unit.Value;
      }
    }
  }
}
