using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using StockDashboard.Infrastructure.CosmosDb.Repositories;
using StockDashboard.Infrastructure.CosmosDb.Settings;

namespace StockDashboard.Infrastructure.CosmosDb {
  internal static class DependencyInjection {
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, IConfiguration configuration) {

      var config = configuration.GetSection("CosmosDb").Get<CosmosDbSettings>();

      var client = new CosmosClient(config.EndpointUrl, config.PrimaryKey, new CosmosClientOptions() {
        SerializerOptions = new CosmosSerializationOptions() {
          PropertyNamingPolicy = CosmosPropertyNamingPolicy.CamelCase
        }
      });
      var cosmosDbClientFactory = new CosmosDbContainerFactory(client, config.DatabaseName, config.Containers);
      cosmosDbClientFactory.EnsureDbSetupAsync().Wait();

      services.AddSingleton<ICosmosDbContainerFactory>(cosmosDbClientFactory);

      services.AddScoped<IExchangeRepository, ExchangeRepository>();
      services.AddScoped<IUserRepository, UserRepository>();

      return services;
    }
  }
}
