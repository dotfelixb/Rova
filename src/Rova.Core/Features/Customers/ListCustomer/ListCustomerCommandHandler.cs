using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Rova.Data.Repository;
using Rova.Model.Domain;

namespace Rova.Core.Features.Customers.ListCustomer
{

    public class ListCustomerCommandHandler
        : IRequestHandler<ListCustomerCommand, ListResult<Customer>>
    {
        private readonly ICustomerRepository _customerRepository;

        public ListCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<ListResult<Customer>> Handle(
            ListCustomerCommand request
            , CancellationToken cancellationToken)
        {

            var rst = await _customerRepository.List(request.Offset, request.Limit);

            return new ListResult<Customer>
            {
                Data = rst,
                Ok = true,
                Type = "list"
            };
        }
    }
}

