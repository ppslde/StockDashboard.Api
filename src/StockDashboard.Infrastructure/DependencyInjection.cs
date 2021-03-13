using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockDashboard.Application.Interfaces;
using StockDashboard.Infrastructure.CosmosDb;
using StockDashboard.Infrastructure.MarketStack;
using System.Reflection;

namespace StockDashboard.Infrastructure {
  public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddScoped<IExchangeRepository, ExchangeRepository>();
      services.AddScoped<IStockService, MarketStackService>();
      
      //services.Configure<MarketStackOptions>(configuration.GetSection(nameof(MarketStackOptions)));

      return services;
    }
  }
}
