using Newtonsoft.Json;

namespace StockDashboardLogic.Services.MarketStack.Objects {
  internal class ExchangeObject {
    public string Name { get; set; }
    public string Acronym { get; set; }
    public string Mic { get; set; }
    public string Country { get; set; }
    [JsonProperty("country_code")]
    public string CountryCode { get; set; }
    public string City { get; set; }
    public string Website { get; set; }
    public CurrencyObject Currency { get; set; }
    public TimeZoneObject TimeZone { get; set; }
  }
}
