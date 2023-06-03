using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddCountryRequestValidator : AbstractValidator<AddCountryRequest>
{
    public AddCountryRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("Country name is required.")
            .Length(1, 250)
            .WithMessage("Country name must be between 1 and 250 characters long.");

        RuleFor(x => x.DateEstablished)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Date established cannot be in the future.");

        RuleFor(x => x.GovernmentType)
            .NotNull()
            .WithMessage("Government type is required.")
            .IsInEnum()
            .WithMessage("Invalid government type.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !String.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Country summary must be between 1 and 10,000 characters long.");
    }
}
