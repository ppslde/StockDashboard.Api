using System;
using System.Collections.Generic;

namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class Response<T> {
    public Pagination Pagination { get; set; }
    public List<T> Data { get; set; }

    public bool IsLastResponse => Pagination.Count < Pagination.Limit;
    public int NextOffset => IsLastResponse ? throw new InvalidOperationException() : Pagination.Offset + Pagination.Count;

    public List<int> AllRequestOffsets() {
      var offsets = new List<int>();
      for (int i = Pagination.Offset + Pagination.MaxLimit; i < Pagination.Total; i += Pagination.MaxLimit) {
        offsets.Add(i);
      }
      return offsets;
    }
  }

  
}
