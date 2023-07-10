using FluentValidation;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class UpdateUserUsernameRequestValidator : AbstractValidator<UpdateUserUsernameRequest>
{
    public UpdateUserUsernameRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Length(4, 20)
            .WithMessage("Username must be between 4 and 20 characters long.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Username must not start or end with whitespace.");
    }
}
