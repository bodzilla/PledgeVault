using System;
using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Models.Validators;

using FluentValidation;

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
