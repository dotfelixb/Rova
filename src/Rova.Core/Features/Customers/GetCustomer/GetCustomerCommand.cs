using System;
using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Customers.GetCustomer
{
    public class GetCustomerCommand : IRequest<SingleResult<Customer>>
    {
        public Guid CustomerId { get; set; }
    }
}

