using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Validators;

using FluentValidation;

public sealed class AddPositionRequestValidator : AbstractValidator<AddPositionRequest>
{
    public AddPositionRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Title.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !string.IsNullOrWhiteSpace(x.Summary));
    }
}
