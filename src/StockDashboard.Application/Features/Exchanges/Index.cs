using MediatR;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Application.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Index {
  
    public class Command: IRequest<IEnumerable<ExchangeModel>> {
    }

    public class CommandHandler : IRequestHandler<Command, IEnumerable<ExchangeModel>> {
      private readonly IExchangeRepository _exchangeRepository;

      public CommandHandler( IExchangeRepository exchangeRepository) {
        _exchangeRepository = exchangeRepository;
      }

      public async Task<IEnumerable<ExchangeModel>> Handle(Command request, CancellationToken cancellationToken) {

        var e = await _exchangeRepository.GetAllExchangesAsync();
        return e.Select(e => new ExchangeModel { Mic = e.Mic, Acronym = e.Acronym, Name = e.Name, Country = e.Country });
      }
    }

  }
}
