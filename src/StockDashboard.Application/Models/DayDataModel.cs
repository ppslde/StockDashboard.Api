using System;

namespace StockDashboard.Application.Models {
  public class DayDataModel: BaseModel {
    public override string Id => $"{ExchangeMic}.{TickerSymbol}.{Date:yyyyMMdd}";
    public string TickerSymbol { get; set; }
    public string ExchangeMic { get; set; }
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    public decimal? AdjustedHigh { get; set; }
    public decimal? AdjustedLow { get; set; }
    public decimal? AdjustedClose { get; set; }
    public decimal? AdjustedOpen { get; set; }
    public decimal? AdjustedVolume { get; set; }
  }
}
