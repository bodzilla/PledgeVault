using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Dtos.Validators;

public sealed class AddCountryRequestValidator : AbstractValidator<AddCountryRequest>
{
    public AddCountryRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .Length(1, 250)
            .WithMessage("Name must be between 1 and 250 characters long.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Name must not start or end with whitespace.");

        RuleFor(x => x.DateEstablished)
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Date Established cannot be in the future.");

        RuleFor(x => x.GovernmentType)
            .NotNull()
            .WithMessage("Government Type is required.")
            .IsInEnum()
            .WithMessage("Invalid Government Type.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !string.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary must be between 1 and 10,000 characters long.");
    }
}
