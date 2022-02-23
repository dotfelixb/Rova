using FluentValidation;

namespace Rova.Core.Features.Customers.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.FirstName)
                .NotEmpty()
                .When(r => r.DisplayName == null);

            RuleFor(r => r.LastName)
                .NotEmpty()
                .When(r => r.DisplayName == null);

            RuleFor(r => r.DisplayName)
                .NotEmpty()
                .When(r => r.FirstName == null && r.LastName == null);
        }
    }
}

