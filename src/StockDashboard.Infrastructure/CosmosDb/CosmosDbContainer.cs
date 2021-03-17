using Microsoft.Azure.Cosmos;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using StockDashboard.Infrastructure.CosmosDb.Settings;

namespace StockDashboard.Infrastructure.CosmosDb {
  public class CosmosDbContainer : ICosmosDbContainer {
    public Container Container { get; }
    public ContainerInfo Info { get; }
    public CosmosDbContainer(CosmosClient cosmosClient, string databaseName, ContainerInfo containerInfo) {
      Info = containerInfo;
      Container = cosmosClient.GetContainer(databaseName, Info.Name);
    }
  }
}
