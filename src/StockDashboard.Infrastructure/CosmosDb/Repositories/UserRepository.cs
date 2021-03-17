using Microsoft.Azure.Cosmos;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces.Persistence;
using StockDashboard.Infrastructure.CosmosDb.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb.Repositories {
  public class UserRepository : CosmosDbRepository<AppUser>, IUserRepository {
    public UserRepository(ICosmosDbContainerFactory cosmosDbContainerFactory) : base(cosmosDbContainerFactory) {
    }

    public override string ContainerName => "users";

    public override string GenerateId(AppUser entity) => $"{entity.Name}";

    public override PartitionKey ResolvePartitionKey(string id) => new(id);

    public async Task<AppUser> GetUserByNameAndPassword(string username, string password) {
      string query = @$"SELECT c.name, c.roles FROM c WHERE c.name=@Name AND c.pwdHash=@Pwd";

      var queryDefinition = new QueryDefinition(query)
                                  .WithParameter("@Name", username)
                                  .WithParameter("@Pwd", password);

      var entities = await GetItemsAsync(queryDefinition);

      return entities.SingleOrDefault();
    }
  }
}
