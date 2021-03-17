using StockDashboard.Application.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockDashboard.Application.Interfaces {
  public interface IStockService {
    Task<IEnumerable<DayData>> GetEndOfDayData(string stockSymbol, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Exchange>> GetExchanges();
    Task<IEnumerable<Ticker>> GetExchangeTickers(string exchangeMic);
    Task<IEnumerable<DayData>> GetIntraDayData(string stockSymbol, DateTime fromDate, DateTime toDate);
    Task<IEnumerable<Ticker>> GetTickersByExchange(string exchangeMic);
    Task<IEnumerable<Ticker>> SearchTicker(string searchToken, string exchangeMic);
  }
}
