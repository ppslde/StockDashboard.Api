using MediatR;
using Microsoft.AspNetCore.Mvc;
using StockDashboard.Application.Features.Authorization;
using StockDashboard.Application.Models;
using System.Threading.Tasks;

namespace StockDashboard.WebApi.Controllers {
  [Route("api/[controller]")]
  [ApiController]
  public class AuthorizationController : ControllerBase {
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator) {
      _mediator = mediator;
    }

    [HttpPost("authorize")]
    public async Task<IActionResult> AuthorizeAsync(AuthModel authUser) {

      var authCommand = new Authorize.Command {
        Username = authUser.Username,
        Password = authUser.Password
      };

      var user = await _mediator.Send(authCommand);
      if (user == null) {
        return BadRequest();
      }

      return Ok(user);
    }
  }
}
