using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockDashboard.Application.Interfaces;
using StockDashboard.Infrastructure.CosmosDb;
using StockDashboard.Infrastructure.MarketStack;
using System.Reflection;

namespace StockDashboard.Infrastructure {
  public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

      services.Configure<MarketStackOptions>(configuration.GetSection("MarketStack"));
      services.Configure<CosmosDbOptions>(configuration.GetSection("CosmosDb"));

      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddScoped<IExchangeRepository, ExchangeRepository>();
      services.AddScoped<IStockService, MarketStackService>();
      
      return services;
    }
  }
}
