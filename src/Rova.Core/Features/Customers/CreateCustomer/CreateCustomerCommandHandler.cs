using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using AutoMapper;
using MassTransit;
using MediatR;
using Rova.Core.Extensions;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Customers.CreateCustomer
{

    public class CreateCustomerCommandHandler
        : IRequestHandler<CreateCustomerCommand, SingleResult<Guid>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CreateCustomerCommandHandler(
            ICustomerRepository customerRepository
            , IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<SingleResult<Guid>> Handle(
            CreateCustomerCommand request
            , CancellationToken cancellationToken)
        {
            var jsonOptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var code = await _customerRepository.GenerateCustomerCode();

            var customer = _mapper.Map<CreateCustomerCommand, Customer>(request);
            customer.Id = NewId.NextGuid();
            // TODO: Handle Prefix
            customer.Code = code.FormatCode("CU");

            // check if displayname is null
            customer.DisplayName ??= $"{customer.FirstName} {customer.LastName}";

            var auditLog = new DbAuditLog
            {
                Id = NewId.NextGuid(),
                TargetId = customer.Id,
                ActionName = "schedule.create",
                ObjectName = "schedule",
                ObjectData = JsonSerializer.Serialize(customer, jsonOptions),
                // TODO: Fix user
                CreatedBy = NewId.NextGuid()
            };

            var rst = await _customerRepository.CreateCustomer(customer, auditLog);
            if (rst < 1)
            {
                return new SingleResult<Guid>
                {
                    Data = Guid.Empty,
                    Ok = false,
                };
            }

            return new SingleResult<Guid>
            {
                Data = customer.Id,
                Ok = true,
                Type = "single"
            };

        }
    }
}

