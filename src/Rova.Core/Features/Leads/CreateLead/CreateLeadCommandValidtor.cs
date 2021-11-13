using FluentValidation;

namespace Rova.Core.Features.Leads.CreateLead
{
    public class CreateLeadCommandValidtor : AbstractValidator<CreateLeadCommand>
    {
        public CreateLeadCommandValidtor()
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