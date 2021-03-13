namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class Pagination {
    public int MaxLimit { get; set; } = 100;
    public int Limit { get; set; }
    public int Offset { get; set; }
    public int Count { get; set; }
    public int Total { get; set; }
  }
}
