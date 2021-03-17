using StockDashboard.Application.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockDashboard.Application.Interfaces.Persistence {
  public interface IRepository<T> where T : Entity {
    Task<IEnumerable<T>> GetItemsAsync(string query);
    //Task<IEnumerable<T>> GetItemsAsync(ISpecification<T> specification);
    //Task<int> GetItemsCountAsync(ISpecification<T> specification);
    Task<T> GetItemAsync(string id);
    Task AddItemAsync(T item);
    Task UpdateItemAsync(T item);
    Task DeleteItemAsync(T item);
  }
}
