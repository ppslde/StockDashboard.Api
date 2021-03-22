using MediatR;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Load {
    public class Command: IRequest<IEnumerable<ExchangeModel>> {
    }

    public class CommandHandler : IRequestHandler<Command, IEnumerable<ExchangeModel>> {
      private readonly IStockService _stocks;
      private readonly IExchangeRepository _exchangeRepository;

      public CommandHandler(IStockService stocks, IExchangeRepository exchangeRepository) {
        _stocks = stocks;
        _exchangeRepository = exchangeRepository;
      }

      public async Task<IEnumerable<ExchangeModel>> Handle(Command request, CancellationToken cancellationToken) {

        var exchanges = await _stocks.GetExchanges();

        foreach (var exchange in exchanges) {
          await _exchangeRepository.UpdateItemAsync(exchange);
        }

        return exchanges.Select(e => new ExchangeModel { Mic = e.Mic, Acronym = e.Acronym, Name = e.Name, Country = e.Country }); ;
      }
    }
  }
}