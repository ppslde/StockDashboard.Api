using AutoMapper;
using StockDashboard.Application.Entities;
using StockDashboardLogic.Services.MarketStack.Objects;

namespace StockDashboard.Infrastructure.MarketStack {
  class MarketStackMappingProfile : Profile {
    public MarketStackMappingProfile() {
      CreateMap<ExchangeObject, Exchange>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); ;
      CreateMap<DayDataObject, DayData>();
      CreateMap<TickerObject, Ticker>();
    }
  }
}
