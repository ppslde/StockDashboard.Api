namespace StockDashboard.Application.Entities {
  public class Exchange : Entity {
    public string Name { get; set; }
    public string Acronym { get; set; }
    public string Mic { get; set; }
    public string Country { get; set; }
    public string CountryCode { get; set; }
    public string City { get; set; }
    public string Website { get; set; }
    public string CurrencyCode { get; set; }
    public string CurrencySymbol { get; set; }
    public string CurrencyName { get; set; }
    public string TimeZoneName { get; set; }
    public string TimeZoneAbbreviation { get; set; }
    public string TimeZoneAbbreviationSummer { get; set; }
  }
}
