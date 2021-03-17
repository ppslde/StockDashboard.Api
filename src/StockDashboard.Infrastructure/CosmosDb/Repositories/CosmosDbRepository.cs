using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : Entity {

    public abstract string ContainerName { get; }
    public abstract string GenerateId(T entity);
    public abstract PartitionKey ResolvePartitionKey(string id);

    private readonly ICosmosDbContainerFactory _cosmosDbContainerFactory;
    private readonly Container _container;

    public CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) {
      _cosmosDbContainerFactory = cosmosDbContainerFactory
        ?? throw new ArgumentNullException(nameof(ICosmosDbContainerFactory));
      _container = _cosmosDbContainerFactory.GetContainer(ContainerName).Container;
    }

    public async Task AddItemAsync(T item) {
      item.Id = GenerateId(item);
      await _container.CreateItemAsync(item, ResolvePartitionKey(item.Id));
    }

    public async Task DeleteItemAsync(T item) {
      await _container.DeleteItemAsync<T>(item.Id, ResolvePartitionKey(item.Id));
    }

    public async Task<T> GetItemAsync(string id) {
      try {

        //TODO: der PartitionKey muss doch irgendwie mit in die ID, da er bei Suche und Löschen benötigt wird :(
        ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id));
        return response.Resource;
      }
      catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound) {
        return null;
      }
    }

    // Search data using SQL query string
    // This shows how to use SQL string to read data from Cosmos DB for demonstration purpose.
    // For production, try to use safer alternatives like Parameterized Query and LINQ if possible.
    // Using string can expose SQL Injection vulnerability, e.g. select * from c where c.id=1 OR 1=1. 
    // String can also be hard to work with due to special characters and spaces when advanced querying like search and pagination is required.
    public async Task<IEnumerable<T>> GetItemsAsync(string queryString) {
      return await GetItemsAsync(new QueryDefinition(queryString));
    }

    protected async Task<IEnumerable<T>> GetItemsAsync(QueryDefinition query) {
      var resultSetIterator = _container.GetItemQueryIterator<T>(query);
      var results = new List<T>();
      while (resultSetIterator.HasMoreResults) {
        var response = await resultSetIterator.ReadNextAsync();
        results.AddRange(response.ToList());
      }
      return results.ToArray();
    }

    public async Task UpdateItemAsync(T item) {
      item.Id = GenerateId(item);
      await _container.UpsertItemAsync(item, ResolvePartitionKey(item.Id));
    }
  }
}
