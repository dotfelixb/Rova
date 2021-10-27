using FluentValidation;

namespace Rova.Core.Features.Customers.GetCustomer
{
    public class GetCustomerCommandValidator : AbstractValidator<GetCustomerCommand>
    {
        public GetCustomerCommandValidator()
        {
            RuleFor(r => r.CustomerId).NotNull();
        }
    }
}

