namespace StockDashboard.Application.Entities {
  public class Ticker : Entity {
    public string ExchangeMic { get; set; }
    public string Symbol { get; set; }
    public string Name { get; set; }
    public bool HasIntraday { get; set; }
    public bool HasEod { get; set; }
  }
}
