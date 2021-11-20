using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Users.CreateUser
{
    public class CreateUserCommandHandler 
        : IRequestHandler<CreateUserCommand, SingleResult<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserCommandHandler(
            IUserRepository userRepository
            , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SingleResult<Guid>> Handle(
            CreateUserCommand request
            , CancellationToken cancellationToken)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var user = _mapper.Map<CreateUserCommand, User>(request);
            user.Id = NewId.NextGuid();
            user.Username ??= user.Email.Split("@")[0];
            user.DisplayName ??= user.Username;
            
            var auditLog = new DbAuditLog
            {
                Id = NewId.NextGuid(),
                TargetId = user.Id,
                ActionName = "users.create",
                ObjectName = "users",
                ObjectData = JsonSerializer.Serialize(user, jsonOptions),
                // TODO: Fix user
                CreatedBy = NewId.NextGuid()
            };

            var rst = await _userRepository.CreateUser(user, auditLog);
            if (rst < 1)
            {
                return new SingleResult<Guid>
                {
                    Data = Guid.Empty,
                    Ok = false
                };
            }

            return new SingleResult<Guid>
            {
                Data = user.Id,
                Ok = true,
                Type = "single"
            };
        }
    }
}