using Newtonsoft.Json;

namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class TimeZoneObject {

    [JsonProperty("timezone")]
    public string Name { get; set; }
    [JsonProperty("abbr")]
    public string Abbreviation { get; set; }
    [JsonProperty("abbr_dst")]
    public string AbbreviationSummer { get; set; }
  }
}
