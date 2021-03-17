using StockDashboard.Application.Entities;
using System.Threading.Tasks;

namespace StockDashboard.Application.Interfaces.Persistence {
  public interface IUserRepository : IRepository<AppUser> {
    Task<AppUser> GetUserByNameAndPassword(string username, string password);
  }
}
