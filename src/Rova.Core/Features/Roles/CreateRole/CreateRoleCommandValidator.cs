using FluentValidation;

namespace Rova.Core.Features.Roles.CreateRole
{
    public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
    {
        public CreateRoleCommandValidator()
        {
            RuleFor(r => r.Rolename)
                .NotEmpty();
        }
    }
}