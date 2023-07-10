using FluentValidation;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class UpdateUserPasswordRequestValidator : AbstractValidator<UpdateUserPasswordRequest>
{
    public UpdateUserPasswordRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.CurrentPassword)
            .NotEmpty()
            .WithMessage("Current Password is required.");

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .WithMessage("New Password is required.")
            .MinimumLength(8)
            .WithMessage("New Password must be at least 8 characters long.");
    }
}
