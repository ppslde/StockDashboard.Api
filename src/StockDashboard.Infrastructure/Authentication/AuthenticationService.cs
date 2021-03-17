using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockDashboard.Application.Interfaces.Authentication;
using StockDashboard.Infrastructure.Authentication.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StockDashboard.Infrastructure.Authentication {
  public class AuthenticationService : IAuthenticationService {

    private readonly AuthenticationSettings _authOptions;
    public AuthenticationService(IOptions<AuthenticationSettings> options) {
      _authOptions = options.Value;
    }

    public string GetPasswordHash(string password) {

      var secretPwd = $"{_authOptions.HashPrefix}{password.Reverse()}{_authOptions.HashSuffix}";
      var inputBytes = Encoding.ASCII.GetBytes(secretPwd);

      using var md5 = MD5.Create();
      var hashBytes = md5.ComputeHash(inputBytes);

      var sb = new StringBuilder();
      for (int i = 0; i < hashBytes.Length; i++) {
        sb.Append(hashBytes[i].ToString("X2"));
      }
      return sb.ToString();
    }

    public string GetUserToken(string username) {

      var key = Encoding.ASCII.GetBytes(_authOptions.PrivateKey);
      var tokenDescriptor = new SecurityTokenDescriptor {
        Subject = new ClaimsIdentity(new Claim[] { new Claim(ClaimTypes.Role, "admin"), new Claim(ClaimTypes.Name, username) }),
        Expires = DateTime.UtcNow.AddDays(_authOptions.ExpireDays),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var tokenHandler = new JwtSecurityTokenHandler();
      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}
