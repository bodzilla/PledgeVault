using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddPoliticianRequestValidator : AbstractValidator<AddPoliticianRequest>
{
    public AddPoliticianRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Name.Trim())
            .NotEmpty()
            .WithMessage("Name cannot be empty.")
            .Length(1, 250)
            .WithMessage("Name length must be between 1 and 250 characters.");

        RuleFor(x => x.SexType)
            .NotNull()
            .WithMessage("Sex Type cannot be null.")
            .IsInEnum()
            .WithMessage("Invalid Sex Type.");

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .WithMessage("Date of Birth cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Date of Birth must be a date in the past.");

        RuleFor(x => x.DateOfDeath)
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.DateOfDeath.HasValue)
            .WithMessage("Date of Death must be a date in the past when it is not null.");

        RuleFor(x => x.CountryOfBirth.Trim())
            .NotEmpty()
            .WithMessage("Country of Birth cannot be empty.")
            .Length(1, 250)
            .WithMessage("Country of Birth length must be between 1 and 250 characters.");

        RuleFor(x => x.PartyId)
            .GreaterThan(0)
            .WithMessage("Party Id must be greater than 0.");

        RuleFor(x => x.Position)
            .NotEmpty()
            .WithMessage("Position cannot be empty.")
            .Length(1, 250)
            .WithMessage("Position length must be between 1 and 250 characters.");

        RuleFor(x => x.PhotoUrl)
            .Length(1, 250)
            .When(x => !String.IsNullOrWhiteSpace(x.PhotoUrl))
            .WithMessage("Photo URL length must be between 1 and 250 characters when Photo URL is not empty.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !String.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary length must be between 1 and 10,000 characters when Summary is not empty.");
    }
}
