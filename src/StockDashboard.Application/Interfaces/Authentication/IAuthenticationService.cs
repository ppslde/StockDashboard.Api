namespace StockDashboard.Application.Interfaces.Authentication {
  public interface IAuthenticationService {
    string GetPasswordHash(string password);
    string GetUserToken(string username);
  }
}
