using AutoMapper;
using StockDashboard.Application.Entities;
using StockDashboardLogic.Services.MarketStack.Objects;

namespace StockDashboard.Infrastructure.MarketStack {
  class MarketStackMappingProfile : Profile {
    public MarketStackMappingProfile() {
      CreateMap<ExchangeObject, Exchange>();
      CreateMap<DayDataObject, DayData>();
      CreateMap<TickerObject, Ticker>();
    }
  }
}
