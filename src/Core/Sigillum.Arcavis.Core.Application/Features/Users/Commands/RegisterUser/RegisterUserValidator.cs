using FluentValidation;

namespace Sigillum.Arcavis.Core.Application.Features.Users.Commands.RegisterUser;

internal class RegisterUserValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Password)
            .MinimumLength(6)
            .WithState(_ => RegisterUserError.PasswordIsTooShort);
    }
}
