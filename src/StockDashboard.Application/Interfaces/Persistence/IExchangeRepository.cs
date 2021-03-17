using StockDashboard.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockDashboard.Application.Interfaces.Persistence {
  public interface IExchangeRepository : IRepository<Exchange> {
    Task<IEnumerable<Exchange>> GetAllExchangesAsync();
  }
}
