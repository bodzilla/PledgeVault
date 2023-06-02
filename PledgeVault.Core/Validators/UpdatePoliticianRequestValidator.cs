using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Validators;

using FluentValidation;
using System;

public sealed class UpdatePoliticianRequestValidator : AbstractValidator<UpdatePoliticianRequest>
{
    public UpdatePoliticianRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Id).GreaterThan(0);
        RuleFor(x => x.Name.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.SexType).NotNull().IsInEnum();
        RuleFor(x => x.DateOfBirth).NotEmpty().LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.DateOfDeath).LessThanOrEqualTo(DateTime.Now).When(x => x.DateOfDeath.HasValue);
        RuleFor(x => x.CountryOfBirth.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.PartyId).GreaterThan(0);
        RuleFor(x => x.Position).NotEmpty().Length(1, 250);
        RuleFor(x => x.PhotoUrl).Length(1, 250).When(x => !string.IsNullOrWhiteSpace(x.PhotoUrl));
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !string.IsNullOrWhiteSpace(x.Summary));
    }
}
