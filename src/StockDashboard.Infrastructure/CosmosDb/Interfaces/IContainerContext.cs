using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;

namespace StockDashboard.Infrastructure.CosmosDb.Interfaces {
  public interface IContainerContext<T> where T : Entity {
    string ContainerName { get; }
    string GenerateId(T entity);
    PartitionKey ResolvePartitionKey(string id);
  }
}
