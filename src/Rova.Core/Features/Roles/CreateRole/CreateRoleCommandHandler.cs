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

namespace Rova.Core.Features.Roles.CreateRole
{
    public class CreateRoleCommandHandler 
        : IRequestHandler<CreateRoleCommand, SingleResult<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateRoleCommandHandler(
            IUserRepository userRepository
            , IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<SingleResult<Guid>> Handle(
            CreateRoleCommand request
            , CancellationToken cancellationToken)
        {  
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var role = _mapper.Map<CreateRoleCommand, Role>(request);
            role.Id = NewId.NextGuid();
            
            var auditLog = new DbAuditLog()
            {
                Id = NewId.NextGuid(),
                TargetId = role.Id,
                ActionName = "roles.create",
                ObjectName = "roles",
                ObjectData = JsonSerializer.Serialize(role, jsonOptions),
                // TODO: Fix user
                CreatedBy = NewId.NextGuid()
            };

            var rst = await _userRepository.CreateRole(role, auditLog);
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
                Data = role.Id,
                Ok = true,
                Type = "single"
            };
        }
    }
}