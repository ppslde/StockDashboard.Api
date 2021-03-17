using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StockDashboard.Infrastructure.Authentication.Settings;
using System.Text;

namespace StockDashboard.Infrastructure.Authentication {
  internal static class DependencyInjection {
    public static IServiceCollection AddBearerAuthConfiguration(this IServiceCollection services, IConfiguration configuration) {

      var section = configuration.GetSection("Security");
      services.Configure<AuthenticationSettings>(section);

      var settings = section.Get<AuthenticationSettings>();
      var key = Encoding.ASCII.GetBytes(settings.PrivateKey);

      services.AddAuthentication(x => {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer(x => {
        x.RequireHttpsMetadata = false;
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = false,
          ValidateAudience = false
        };
      });
      return services;
    }
  }
}
