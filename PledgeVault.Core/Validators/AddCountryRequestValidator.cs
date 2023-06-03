using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddCountryRequestValidator : AbstractValidator<AddCountryRequest>
{
    public AddCountryRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Name.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.DateEstablished).LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.GovernmentType).NotNull().IsInEnum();
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.Summary));
    }
}
