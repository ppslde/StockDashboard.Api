using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace StockDashboard.Application {
  [ExcludeFromCodeCoverage]
  public static class DependencyInjection {

    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration) {

      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
      services.AddMediatR(Assembly.GetExecutingAssembly());

      return services;
    }
  }
}
