using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddResourceRequestValidator : AbstractValidator<AddResourceRequest>
{
    public AddResourceRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Title.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.SiteUrl.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.ResourceType).NotNull().IsInEnum();
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.Summary));
        RuleFor(x => x.PledgeId).GreaterThan(0);
    }
}
