using AutoMapper;
using Microsoft.Extensions.Options;
using StockDashboard.Application.Entities;
using StockDashboard.Application.Interfaces;
using StockDashboard.Infrastructure.MarketStack.Extensions;
using StockDashboardLogic.Services.MarketStack.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Throttling;

namespace StockDashboard.Infrastructure.MarketStack {
  public class MarketStackService : IStockService {
    private readonly IMapper _mapper;
    private readonly MarketStackOptions _options;
    private readonly HttpClient _httpClient;
    private readonly Throttled _throttled;
    private readonly string _apiUrl;

    public MarketStackService(IMapper mapper, IOptions<MarketStackOptions> options, HttpClient httpClient) {
      _mapper = mapper;
      _options = options.Value;

      if (_options.MaxRequestsPerSecond >= 10) {
        _throttled = new Throttled(_options.MaxRequestsPerSecond / 10, 100);
      }
      else {
        _throttled = new Throttled(_options.MaxRequestsPerSecond, 1000);
      }

      _httpClient = httpClient;
      _httpClient.Timeout = TimeSpan.FromMinutes(10);
      _apiUrl = _options.Https ? "https://api.marketstack.com/v1" : "http://api.marketstack.com/v1";
    }

    public async Task<IEnumerable<Exchange>> GetExchanges() {
      var eo = await _httpClient.RequestMarketStackAsync<ExchangeObject>($"{_apiUrl}/exchanges", _options, _throttled);
      return _mapper.ProjectTo<Exchange>(eo.Where(e => !string.IsNullOrEmpty(e.Acronym)).AsQueryable());
    }

    public async Task<IEnumerable<Ticker>> GetTickersByExchange(string exchangeMic) {
      var to = await _httpClient.RequestMarketStackAsync<TickerObject>($"{_apiUrl}/exchanges/{exchangeMic}/tickers", _options, _throttled);
      return _mapper.ProjectTo<Ticker>(to.AsQueryable());
    }

    public async Task<IEnumerable<Ticker>> GetExchangeTickers(string exchangeMic) {
      var to = await _httpClient.RequestMarketStackAsync<TickerObject>($"{_apiUrl}/tickers?exchange={exchangeMic}", _options, _throttled);
      return _mapper.ProjectTo<Ticker>(to.AsQueryable());
    }

    public async Task<IEnumerable<DayData>> GetEndOfDayData(string stockSymbol, DateTime fromDate, DateTime toDate) {
      var ddo = await _httpClient.RequestMarketStackAsync<DayDataObject>($"{_apiUrl}/eod?symbols={stockSymbol}&date_from={fromDate:yyyy-MM-dd}&date_to={toDate:yyyy-MM-dd}", _options, _throttled);
      return _mapper.ProjectTo<DayData>(ddo.AsQueryable());
    }

    public async Task<IEnumerable<DayData>> GetIntraDayData(string stockSymbol, DateTime fromDate, DateTime toDate) {
      var ido = await _httpClient.RequestMarketStackAsync<DayDataObject>($"{_apiUrl}/intraday?symbols={stockSymbol}&date_frogm={fromDate:yyyy-MM-dd HH:mm:ss}&date_to={toDate:yyyy-MM-dd HH:mm:ss}", _options, _throttled);
      return _mapper.ProjectTo<DayData>(ido.AsQueryable());
    }

    public async Task<IEnumerable<Ticker>> SearchTicker(string searchToken, string exchangeMic) {

      var url = string.IsNullOrEmpty(exchangeMic) ?
          $"{_apiUrl}/tickers?search={searchToken}" :
          $"{_apiUrl}/tickers?search={searchToken}&exchange={exchangeMic}";

      var to = await _httpClient.RequestMarketStackAsync<TickerObject>(url, _options, _throttled);
      return _mapper.ProjectTo<Ticker>(to.AsQueryable());
    }
  }
}
