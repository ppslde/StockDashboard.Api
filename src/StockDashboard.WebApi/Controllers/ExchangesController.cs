using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.Application.Features.Exchanges;
using System.Threading.Tasks;

namespace StockDashboard.WebApi.Controllers {
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ExchangesController : ControllerBase {

    private readonly IMediator _mediator;

    public ExchangesController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetExchanges() {

      var exchanges = await _mediator.Send(new Index.Command());
      if (exchanges == null) {
        return BadRequest();
      }

      return Ok(exchanges);
    }


    [HttpGet("load")]
    public async Task<IActionResult> LoadExchanges() {

      var exchanges = await _mediator.Send(new Load.Command());
      if (exchanges == null) {
        return BadRequest();
      }

      return Ok(exchanges);
    }
  }
}
