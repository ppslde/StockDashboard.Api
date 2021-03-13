using Newtonsoft.Json;
using StockDashboardLogic.Services.MarketStack.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Throttling;

namespace StockDashboard.Infrastructure.MarketStack.Extensions {
  public static class HttpExtensions {
    public static async Task<IEnumerable<T>> RequestMarketStackAsync<T>(this HttpClient httpClient, string url, MarketStackOptions options, Throttled throttled) {

      var builder = new UriBuilder(url);
      var query = HttpUtility.ParseQueryString(builder.Query);
      query["access_key"] = options.ApiToken;
      builder.Query = query.ToString();
      var firstResponse = await throttled.Run(() => httpClient.GetPageResponse<T>(builder, options.ItemsPerPage));
      var offsets = firstResponse.AllRequestOffsets();
      var tasks = offsets.Select(async (offset) => await throttled.Run(() => httpClient.GetPageResponse<T>(new UriBuilder(builder.Uri), options.ItemsPerPage, offset)));
      var pages = await Task.WhenAll(tasks);

      var data = pages.SelectMany(page => page.Data).ToArray();
      return data.Concat(firstResponse.Data);
    }

    private static async Task<Response<T>> GetPageResponse<T>(this HttpClient httpClient, UriBuilder builder, int limit, int offset = 0) {
      var query = HttpUtility.ParseQueryString(builder.Query);
      query["offset"] = offset.ToString();
      query["limit"] = limit.ToString();
      builder.Query = query.ToString();

      using Stream s = await httpClient.GetStreamAsync(builder.Uri);
      using StreamReader sr = new StreamReader(s);
      using JsonReader reader = new JsonTextReader(sr);
      JsonSerializer serializer = new JsonSerializer();
      return serializer.Deserialize<Response<T>>(reader);
    }
  }
}
