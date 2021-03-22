using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.Application.Features.Tickers;
using StockDashboard.Application.Models;
using System.Threading.Tasks;

namespace StockDashboard.WebApi.Controllers {
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class TickerController : ControllerBase {

    private readonly IMediator _mediator;

    public TickerController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpGet("search/{token}/exchange/{mic}")]
    public async Task<IActionResult> SearchTicker(string token, string mic) {

      var cmd = new Search.Command { SearchToken = token, Mic = mic };

      var tickers = await _mediator.Send(cmd);
      if (tickers == null) {
        return BadRequest();
      }

      return Ok(tickers);
    }

    [HttpPost]
    public async Task<IActionResult> AddTicker(TickerModel ticker) {

      var cmd = new Add.Command { Ticker = ticker };
      var cnt = await  _mediator.Send(cmd);

      return Ok(cnt);
    }
  }
}
