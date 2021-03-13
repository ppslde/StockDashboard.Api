using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using StockDashboard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockDashboard.Infrastructure.CosmosDb {
  public class ExchangeRepository: IExchangeRepository {

    private readonly CosmosDbOptions _options;

    public ExchangeRepository(IOptions<CosmosDbOptions> options) {
      _options = options.Value;
    }

  }
}
