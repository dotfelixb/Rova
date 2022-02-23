using MediatR;
using Rova.Model.ViewDomain;

namespace Rova.Core.Features.Users.Login
{
    public class LoginCommand : IRequest<SingleResult<LoginResult>> {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
