using FluentValidation;

namespace Rova.Core.Features.Customers.CreateCustomer
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(r => r.FirstName)
                .NotNull()
                .When(r => r.DisplayName == null);

            RuleFor(r => r.LastName)
                .NotNull()
                .When(r => r.DisplayName == null);

            RuleFor(r => r.DisplayName)
                .NotNull()
                .When(r => r.FirstName == null && r.LastName == null);
        }
    }
}

