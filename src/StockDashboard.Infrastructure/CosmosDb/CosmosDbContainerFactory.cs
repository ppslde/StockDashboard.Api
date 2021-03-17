using Microsoft.Azure.Cosmos;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using StockDashboard.Infrastructure.CosmosDb.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb {
  public class CosmosDbContainerFactory : ICosmosDbContainerFactory {

    private readonly CosmosClient _cosmosClient;
    private readonly string _databaseName;
    private readonly List<ContainerInfo> _containers;

    public CosmosDbContainerFactory(CosmosClient cosmosClient,
                               string databaseName,
                               List<ContainerInfo> containers) {
      _databaseName = databaseName ?? throw new ArgumentNullException(nameof(databaseName));
      _containers = containers ?? throw new ArgumentNullException(nameof(containers));
      _cosmosClient = cosmosClient ?? throw new ArgumentNullException(nameof(cosmosClient));
    }

    public ICosmosDbContainer GetContainer(string containerName) {

      var info = _containers.SingleOrDefault(x => x.Name == containerName);
      if (info == null) {
        throw new ArgumentException($"Unable to find container: {containerName}");
      }

      return new CosmosDbContainer(_cosmosClient, _databaseName, info);
    }

    public async Task EnsureDbSetupAsync() {
      var database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseName);

      foreach (ContainerInfo container in _containers) {
        await database.Database.CreateContainerIfNotExistsAsync(container.Name, $"{container.PartitionKey}");
      }
    }
  }
}
