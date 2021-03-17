using Microsoft.Azure.Cosmos;
using StockDashboard.Infrastructure.CosmosDb.Settings;

namespace StockDashboard.Infrastructure.CosmosDb.Interfaces {
  public interface ICosmosDbContainer {
    Container Container { get; }
    ContainerInfo Info { get; }
  }
}
