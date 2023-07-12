using FluentValidation;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class UpdateUserEmailRequestValidator : AbstractValidator<UpdateUserEmailRequest>
{
    public UpdateUserEmailRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .MaximumLength(50)
            .WithMessage("Email must at most 50 characters long.")
            .EmailAddress()
            .WithMessage("Email must be a valid email address.");
    }
}
