using StockDashboard.Application.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockDashboard.Application.Interfaces {
  public interface IStockService {
    Task<IEnumerable<DayDataModel>> GetEndOfDayData(string stockSymbol, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<ExchangeModel>> GetExchanges();
    Task<IEnumerable<TickerModel>> GetExchangeTickers(string exchangeMic);
    Task<IEnumerable<DayDataModel>> GetIntraDayData(string stockSymbol, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<TickerModel>> GetTickersByExchange(string exchangeMic);
    Task<IEnumerable<TickerModel>> SearchTicker(string searchToken, string exchangeMic);
  }
}
