using Newtonsoft.Json;

namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class TickerObject {
    public string Name { get; set; }
    public string Symbol { get; set; }
    [JsonProperty("has_intraday")]
    public bool HasIntraday { get; set; }
    [JsonProperty("has_eod")]
    public bool HasEod { get; set; }
    [JsonProperty("stock_exchange")]
    public ExchangeObject Exchange { get; set; }
  }
}
