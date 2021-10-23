using MediatR;
using Rova.Model.Domain;
using System;

namespace Rova.Core.Features.Customers.CreateCustomer
{
    public class CreateCustomerCommand : Customer, IRequest<SingleResult<Guid>>
    {
        public new string ParentCustomer { get; set; }
        public new string OpeningBalance { get; set; }
        public new string OpeningBalanceAt { get; set; }
    }
}

