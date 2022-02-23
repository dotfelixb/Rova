using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Users.ListUser
{
    public class ListUserCommandHandler
        : IRequestHandler<ListUserCommand, ListResult<UserExtended>>
    {
        private readonly IUserRepository _userRepository;

        public ListUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository; 
        }

        public async Task<ListResult<UserExtended>> Handle(
            ListUserCommand request
            , CancellationToken cancellationToken)
        {
            var rst = await _userRepository.ListUser(request.Offset, request.Limit);

            return new ListResult<UserExtended>
            {
                Data = rst,
                Ok = true,
                Type = "list"
            };
        }
    }
}