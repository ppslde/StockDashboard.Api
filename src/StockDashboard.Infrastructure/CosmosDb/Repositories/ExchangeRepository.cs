using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public class ExchangeRepository : CosmosDbRepository<Exchange>, IExchangeRepository {
    public ExchangeRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory) {
    }

    public override string ContainerName => "exchanges";
    public override string GenerateId(Exchange entity) => $"{entity.Mic}-{entity.Acronym}";
    public override PartitionKey ResolvePartitionKey(string id) => new(id.Split('-')[0]);

    public async Task<IEnumerable<Exchange>> GetAllExchangesAsync() {

      string query = @$"SELECT c.mic, c.acronym, c.name, c.country FROM c";
      var queryDefinition = new QueryDefinition(query);

      var entities = await GetItemsAsync(queryDefinition);

      return entities.ToArray();
    }
  }
}
