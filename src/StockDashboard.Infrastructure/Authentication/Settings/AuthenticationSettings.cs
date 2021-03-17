namespace StockDashboard.Infrastructure.Authentication.Settings {
  public class AuthenticationSettings {
    public string PrivateKey { get; set; }
    public string HashPrefix { get; set; }
    public string HashSuffix { get; set; }
    public bool HashReverse { get; set; }
    public int ExpireDays { get; set; }
  }
}
