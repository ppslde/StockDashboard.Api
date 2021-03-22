using MediatR;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Application.Models;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Exchanges {
  public class Details {

    public class Command : IRequest<ExchangeDetailsModel> {
      public string Mic { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, ExchangeDetailsModel> {

      private readonly IExchangeRepository _exchangeRepository;

      public CommandHandler(IExchangeRepository exchangeRepository) {
        _exchangeRepository = exchangeRepository;
      }

      public async Task<ExchangeDetailsModel> Handle(Command request, CancellationToken cancellationToken) {
        var wx = await _exchangeRepository.GetExchangeByMic(request.Mic);

        return new ExchangeDetailsModel {
          Mic = wx.Mic,
          Name = wx.Name,
          Acronym = wx.Acronym,
          City = wx.City,
          Country = wx.Country,
          CurrencyCode = wx.CurrencyCode,
          CurrencySymbol = wx.CurrencySymbol,
          TimeZoneName = wx.TimeZoneName,
          Website = wx.Website
        };
      }
    }
  }
}
