using System.Collections.Generic;

namespace StockDashboard.Application.Entities {
  public class AppUser : Entity {
    public string Name { get; set; }
    public string PwdHash { get; set; }
    public IEnumerable<string> Roles { get; set; }
  }
}
