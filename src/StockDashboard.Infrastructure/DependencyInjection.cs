using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockDashboard.Application.Interfaces;
using StockDashboard.Application.Interfaces.Authentication;
using StockDashboard.Infrastructure.Authentication;
using StockDashboard.Infrastructure.CosmosDb;
using StockDashboard.Infrastructure.MarketStack;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StockDashboard.Infrastructure {
  [ExcludeFromCodeCoverage]
  public static class DependencyInjection {

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration) {

      services
        .AddCosmosDb(configuration)
        .AddBearerAuthConfiguration(configuration)
        .AddAutoMapper(Assembly.GetExecutingAssembly())
        .Configure<MarketStackOptions>(configuration.GetSection("MarketStack"));

      services
        .AddScoped<IStockService, MarketStackService>()
        .AddScoped<IAuthenticationService, AuthenticationService>();

      return services;
    }
  }
}
