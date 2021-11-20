using System;
using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Users.CreateUser
{
    public class CreateUserCommand : User, IRequest<SingleResult<Guid>>
    {
    }
}