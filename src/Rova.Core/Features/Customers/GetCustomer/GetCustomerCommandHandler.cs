using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Customers.GetCustomer
{

    public class GetCustomerCommandHandler
        : IRequestHandler<GetCustomerCommand, SingleResult<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public GetCustomerCommandHandler(
            ICustomerRepository customerRepository
            , IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<SingleResult<Customer>> Handle(
            GetCustomerCommand request
            , CancellationToken cancellationToken)
        {
            var rst = await _customerRepository.Get(request.CustomerId);

            if (rst.Id == Guid.Empty)
            {
                return new SingleResult<Customer>
                {
                    Data = rst,
                    Errors = new[]  {  $"Customer with Id '{request.CustomerId}' not found" }
                };
            }

            return new SingleResult<Customer>
            {
                Data = rst,
                Ok = true,
                Type = "single"
            };
        }
    }
}

