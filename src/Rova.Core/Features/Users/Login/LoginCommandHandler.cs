using MediatR;
using Rova.Model.ViewDomain;

namespace Rova.Core.Features.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, SingleResult<LoginResult>>
    {
        public Task<SingleResult<LoginResult>> Handle(
            LoginCommand request
            , CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
