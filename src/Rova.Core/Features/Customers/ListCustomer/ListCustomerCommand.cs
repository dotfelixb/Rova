using MediatR;
using Rova.Model.Domain;

namespace Rova.Core.Features.Customers.ListCustomer
{
    public class ListCustomerCommand : IRequest<ListResult<Customer>>
    {
        public int Offset { get; set; } = 0;
        public int Limit { get; set; } = 1000;
    }
}

