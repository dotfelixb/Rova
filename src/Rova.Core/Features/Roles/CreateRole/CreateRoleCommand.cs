using System;
using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Roles.CreateRole
{
    public class CreateRoleCommand : Role, IRequest<SingleResult<Guid>>
    {
        
    }
}