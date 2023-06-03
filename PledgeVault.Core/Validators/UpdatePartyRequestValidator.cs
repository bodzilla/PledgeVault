using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class UpdatePartyRequestValidator : AbstractValidator<UpdatePartyRequest>
{
    public UpdatePartyRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.DateEstablished).NotEmpty().LessThanOrEqualTo(DateTime.Now).When(x => x.DateEstablished.HasValue);
        RuleFor(x => x.CountryId).GreaterThan(0);
        RuleFor(x => x.LogoUrl.Trim()).Length(1, 250).When(x => !String.IsNullOrWhiteSpace(x.LogoUrl));
        RuleFor(x => x.SiteUrl.Trim()).Length(1, 250).When(x => !String.IsNullOrWhiteSpace(x.SiteUrl));
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.Summary));
    }
}
