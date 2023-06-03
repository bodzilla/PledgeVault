using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddPartyRequestValidator : AbstractValidator<AddPartyRequest>
{
    public AddPartyRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .Length(1, 250)
            .WithMessage("Name length must be between 1 and 250 characters.");

        RuleFor(x => x.DateEstablished)
            .NotEmpty()
            .WithMessage("Date Established cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.DateEstablished.HasValue)
            .WithMessage("Date Established must be a date in the past.");

        RuleFor(x => x.CountryId)
            .GreaterThan(0)
            .WithMessage("CountryId must be greater than 0.");

        RuleFor(x => x.LogoUrl.Trim())
            .Length(1, 250)
            .When(x => !String.IsNullOrWhiteSpace(x.LogoUrl))
            .WithMessage("LogoUrl length must be between 1 and 250 characters.");

        RuleFor(x => x.SiteUrl.Trim())
            .Length(1, 250)
            .When(x => !String.IsNullOrWhiteSpace(x.SiteUrl))
            .WithMessage("SiteUrl length must be between 1 and 250 characters.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !String.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary length must be between 1 and 10,000 characters.");
    }
}
