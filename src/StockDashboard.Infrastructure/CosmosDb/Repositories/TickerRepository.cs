using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public class TickerRepository : CosmosDbRepository<Ticker>, ITickerRepository {

    public TickerRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory) {
    }
    public override string ContainerName => "tickers";

    public override string GenerateId(Ticker entity) => $"{entity.ExchangeMic}-{entity.Symbol}";

    public override PartitionKey ResolvePartitionKey(string id) => new(id.Split('-')[0]);

  }
}
