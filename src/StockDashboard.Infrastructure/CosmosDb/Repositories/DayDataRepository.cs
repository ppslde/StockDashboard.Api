using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public class DayDataRepository : CosmosDbRepository<DayData>, IDayDataRepository {
    public DayDataRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory) { }

    public override string ContainerName => "daydata";

    public override string GenerateId(DayData entity) => $"{entity.TickerSymbol}-{entity.Date:yyyyMMdd}";

    public override PartitionKey ResolvePartitionKey(string id) => new(id.Split('-')[0]);
  }
}
