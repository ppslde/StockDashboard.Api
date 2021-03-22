using MediatR;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Application.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Tickers {
  public class Add {

    public class Command : IRequest<int> {
      public TickerModel Ticker { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, int> {

      private readonly IStockService _stocks;
      private readonly ITickerRepository _tickerRepository;
      private readonly IDayDataRepository _dayDataRepository;

      public CommandHandler(IStockService stocks, ITickerRepository tickerRepository, IDayDataRepository dayDataRepository) {
        _stocks = stocks;
        _tickerRepository = tickerRepository;
        _dayDataRepository = dayDataRepository;
      }

      public async Task<int> Handle(Command request, CancellationToken cancellationToken) {

        var newTicker = new Ticker {
          ExchangeMic = request.Ticker.ExchangeMic,
          Symbol = request.Ticker.Symbol,
          Name = request.Ticker.Name,
          HasEod = request.Ticker.HasEod,
          HasIntraday = request.Ticker.HasIntraday
        };
        await _tickerRepository.UpdateItemAsync(newTicker);

        var daydata = await _stocks.GetEndOfDayData(request.Ticker.Symbol, DateTime.Now.AddYears(-20), DateTime.Now);

        foreach (var dataitem in daydata) {
          await _dayDataRepository.UpdateItemAsync(dataitem);
        }

        return daydata.Count();
      }
    }
  }
}
