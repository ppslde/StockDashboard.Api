using MediatR;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Interfaces.Persistence;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Load {
    public class Command: IRequest<IEnumerable<Exchange>> {
    }

    public class CommandHandler : IRequestHandler<Command, IEnumerable<Exchange>> {
      private readonly IStockService _stocks;
      private readonly IExchangeRepository _exchangeRepository;

      public CommandHandler(IStockService stocks, IExchangeRepository exchangeRepository) {
        _stocks = stocks;
        _exchangeRepository = exchangeRepository;
      }

      public async Task<IEnumerable<Exchange>> Handle(Command request, CancellationToken cancellationToken) {

        var exchanges = await _stocks.GetExchanges();

        foreach (var exchange in exchanges) {
          await _exchangeRepository.UpdateItemAsync(exchange);
        }

        return exchanges;
      }
    }
  }
}