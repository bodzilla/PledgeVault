using System;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Validators;

using FluentValidation;

public sealed class UpdatePositionRequestValidator : AbstractValidator<UpdatePositionRequest>
{
    public UpdatePositionRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Title.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !string.IsNullOrWhiteSpace(x.Summary));
    }
}
