using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Roles.ListRole
{
    public class ListRoleCommandHandler 
        : IRequestHandler<ListRoleCommand, ListResult<RoleExtended>>
    {
        private readonly IUserRepository _userRepository;

        public ListRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ListResult<RoleExtended>> Handle(
            ListRoleCommand request
            , CancellationToken cancellationToken)
        {
            var rst = await _userRepository.ListRole(request.Offset, request.Limit);

            return new ListResult<RoleExtended>
            {
                Data = rst,
                Ok = true,
                Type = "list"
            };
        }
    }
}