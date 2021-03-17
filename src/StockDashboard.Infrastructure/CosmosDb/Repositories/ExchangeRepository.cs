using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using System;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public class ExchangeRepository : CosmosDbRepository<Exchange>, IExchangeRepository {
    public ExchangeRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory) {
    }

    public override string ContainerName => "exchanges";
    public override string GenerateId(Exchange entity) => $"{entity.Mic}-{entity.Acronym}";
    public override PartitionKey ResolvePartitionKey(string id) => new(id.Split('-')[0]);
  }
}
