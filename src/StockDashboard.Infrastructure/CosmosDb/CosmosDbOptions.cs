namespace StockDashboard.Infrastructure.CosmosDb {
  public class CosmosDbOptions {
    public string DatabaseName { get; set; }
    public string ContainerName { get; set; }
    public string Account { get; set; }
    public string Key { get; set; }
  }
}
