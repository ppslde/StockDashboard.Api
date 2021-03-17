using MediatR;
using StockDashboard.Application.Interfaces.Persistence;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Index {
  
    public class Command: IRequest<IEnumerable<Model>> {
    }

    public class Model {
      public string Mic { get; set; }
      public string Name { get; set; }
      public string Acronym { get; set; }
      public string Country { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, IEnumerable<Model>> {
      private readonly IExchangeRepository _exchangeRepository;

      public CommandHandler( IExchangeRepository exchangeRepository) {
        _exchangeRepository = exchangeRepository;
      }

      public async Task<IEnumerable<Model>> Handle(Command request, CancellationToken cancellationToken) {

        var e = await _exchangeRepository.GetAllExchangesAsync();
        return e.Select(e => new Model { Mic = e.Mic, Acronym = e.Acronym, Name = e.Name, Country = e.Country });
      }
    }

  }
}
