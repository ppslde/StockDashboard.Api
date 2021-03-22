using System;

namespace StockDashboard.Application.Entities {
  public class DayData : Entity {
    public string TickerSymbol { get; set; }
    public string ExchangeMic { get; set; }
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
  }
}
