namespace StockDashboard.Application.Models {
  public class TickerModel:BaseModel {
    public override string Id => $"{ExchangeMic}.{Symbol}";
    public string ExchangeMic { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public bool HasIntraday { get; set; }
    public bool HasEod { get; set; }
  }
}
