using MediatR;
using StockDashboard.Application.Interfaces.Authentication;
using StockDashboard.Application.Interfaces.Persistence;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StockDashboard.Application.Features.Authorization {
  public class Authorize {
    public class Command : IRequest<Model> {
      public string Username { get; set; }
      public string Password { get; set; }
    }

    public class Model {
      public string User { get; set; }
      public string Token { get; set; }
      public string[] Roles { get; set; }
    }

    public class CommandHandler : IRequestHandler<Command, Model> {
      private readonly IAuthenticationService _authentication;
      private readonly IUserRepository _userRepository;

      public CommandHandler(IAuthenticationService authentication, IUserRepository userRepository) {
        _authentication = authentication;
        _userRepository = userRepository;
      }

      public async Task<Model> Handle(Command request, CancellationToken cancellationToken) {

        var user = await _userRepository.GetUserByNameAndPassword(request.Username, _authentication.GetPasswordHash(request.Password));

        if (user == null) {
          return null;
        }

        return new Model {
          User = user.Name,
          Token = _authentication.GetUserToken(user.Name),
          Roles = user.Roles?.ToArray()
        };
      }
    }
  }
}
