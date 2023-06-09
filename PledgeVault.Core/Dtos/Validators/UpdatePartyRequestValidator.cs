using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class UpdatePartyRequestValidator : AbstractValidator<UpdatePartyRequest>
{
    public UpdatePartyRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .Length(1, 250)
            .WithMessage("Name length must be between 1 and 250 characters.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Name must not start or end with whitespace.");

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
            .When(x => !string.IsNullOrWhiteSpace(x.LogoUrl))
            .WithMessage("Logo URL length must be between 1 and 250 characters.");

        RuleFor(x => x.SiteUrl.Trim())
            .Length(1, 250)
            .When(x => !string.IsNullOrWhiteSpace(x.SiteUrl))
            .WithMessage("Site URL length must be between 1 and 250 characters.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !string.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary length must be between 1 and 10,000 characters.");
    }
}
