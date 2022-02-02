using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Users.ListUser
{
    public class ListUserCommand
        : DbList, IRequest<ListResult<UserExtended>>
    {
        
    }
}