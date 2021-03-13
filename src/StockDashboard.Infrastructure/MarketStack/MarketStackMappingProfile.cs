using AutoMapper;
using StockDashboard.Application.Models;
using StockDashboardLogic.Services.MarketStack.Objects;

namespace StockDashboard.Infrastructure.MarketStack {
  class MarketStackMappingProfile : Profile {
    public MarketStackMappingProfile() {
      CreateMap<ExchangeObject, ExchangeModel>();
      CreateMap<DayDataObject, DayDataModel>();
      CreateMap<TickerObject, TickerModel>();
    }
  }
}
