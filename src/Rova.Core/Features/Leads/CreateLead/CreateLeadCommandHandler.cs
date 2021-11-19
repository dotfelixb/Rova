using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MassTransit;
using MediatR;
using Rova.Core.Extensions;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Leads.CreateLead
{
    public class CreateLeadCommand : Lead, IRequest<SingleResult<Guid>>
    {
        public new string Campaign { get; set; }
    }
    
    public class CreateLeadCommandHandler 
        : IRequestHandler<CreateLeadCommand, SingleResult<Guid>>
    {
        private readonly ILeadRepository _leadRepository;
        private readonly IMapper _mapper;

        public CreateLeadCommandHandler(ILeadRepository leadRepository, IMapper mapper)
        {
            _leadRepository = leadRepository;
            _mapper = mapper;
        }

        public async Task<SingleResult<Guid>> Handle(
            CreateLeadCommand request
            , CancellationToken cancellationToken)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var code = await _leadRepository.GenerateLeadCode();
            var lead = _mapper.Map<CreateLeadCommand, Lead>(request);
            lead.Id = NewId.NextGuid();
            lead.Code = code.FormatCode("LD");

            lead.DisplayName ??= $"{lead.FirstName} {lead.LastName}";

            var auditLog = new DbAuditLog()
            {
                Id = NewId.NextGuid(),
                TargetId = lead.Id,
                ActionName = "lead.create",
                ObjectName = "lead",
                ObjectData = JsonSerializer.Serialize(lead, jsonOptions),
                // TODO: Fix user
                CreatedBy = NewId.NextGuid()
            };

            var rst = await _leadRepository.CreateLead(lead, auditLog);
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
                Data = lead.Id,
                Ok = true,
                Type = "single"
            };
        }
    }
}