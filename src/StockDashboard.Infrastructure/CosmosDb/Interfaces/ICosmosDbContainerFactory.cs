using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb.Interfaces {
  public interface ICosmosDbContainerFactory {
    ICosmosDbContainer GetContainer(string containerName);
    Task EnsureDbSetupAsync();
  }
}
