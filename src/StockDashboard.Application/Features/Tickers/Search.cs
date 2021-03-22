using MediatR;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Tickers {
  public class Search {

    public class Command : IRequest<IEnumerable<TickerModel>> {
      public string SearchToken { get; set; }
      public string Mic { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, IEnumerable<TickerModel>> {

      private readonly IStockService _stocks;
     
      public CommandHandler(IStockService stocks) {
        _stocks = stocks;
      }

      public async Task<IEnumerable<TickerModel>> Handle(Command request, CancellationToken cancellationToken) {
        var tickers = await _stocks.SearchTicker(request.SearchToken, request.Mic);

        return tickers.Select(t => new TickerModel {
          ExchangeMic = t.ExchangeMic,
          Symbol = t.Symbol,
          Name = t.Name,
          HasEod = t.HasEod,
          HasIntraday = t.HasIntraday
        });
      }
    }
  }
}
