using FluentValidation;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
{
    public UpdateCommentRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("UserId must be greater than 0.");

        RuleFor(x => x.PledgeId)
            .GreaterThan(0)
            .WithMessage("PledgeId must be greater than 0.");

        RuleFor(x => x.Text)
            .NotEmpty()
            .WithMessage("Text is required.")
            .Length(1, 500)
            .WithMessage("Text must be between 1 and 500 characters long.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Text must not start or end with whitespace.");
    }
}
