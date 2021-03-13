namespace StockDashboard.Infrastructure.MarketStack {
  public class MarketStackOptions {
    public string ApiToken { get; set; }
    public int MaxRequestsPerSecond { get; set; } = 1;
    public bool Https { get; set; } = true;
    public int ItemsPerPage { get; } = 100;
  }
}
