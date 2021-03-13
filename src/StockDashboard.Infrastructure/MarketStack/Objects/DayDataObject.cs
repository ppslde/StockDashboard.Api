using Newtonsoft.Json;
using System;

namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class DayDataObject {
    public DateTime Date { get; set; }
    public decimal Open { get; set; }
    public decimal High { get; set; }
    public decimal Low { get; set; }
    public decimal Close { get; set; }
    public decimal Volume { get; set; }
    [JsonProperty("symbol")]
    public string TickerSymbol { get; set; }
    [JsonProperty("exchange")]
    public string ExchangeMic { get; set; }

    [JsonProperty("adj_high")]
    public decimal? AdjustedHigh { get; set; }

    [JsonProperty("adj_low")]
    public decimal? AdjustedLow { get; set; }

    [JsonProperty("adj_close")]
    public decimal? AdjustedClose { get; set; }

    [JsonProperty("adj_open")]
    public decimal? AdjustedOpen { get; set; }

    [JsonProperty("adj_volume")]
    public decimal? AdjustedVolume { get; set; }
  }
}
