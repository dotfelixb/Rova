using FluentValidation;

namespace Rova.Core.Features.Users.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(r => r.Username)
                .Custom((val, ctx) =>
                {
                    if (val is string && val.Contains(' '))
                    {
                        ctx.AddFailure("Username", "'Username' can't contain space");
                    }
                });
            RuleFor(r => r.Email)
                .NotNull()
                .EmailAddress();
        }
    }
}