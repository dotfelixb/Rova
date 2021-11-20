using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Roles.ListRole
{
    public class ListRoleCommand 
        : DbList, IRequest<ListResult<RoleExtended>>
    {
    }
}