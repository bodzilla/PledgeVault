using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddResourceRequestValidator : AbstractValidator<AddResourceRequest>
{
    public AddResourceRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(1, 250)
            .WithMessage("Title length must be between 1 and 250 characters.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Title must not start or end with whitespace.");

        RuleFor(x => x.SiteUrl)
            .NotEmpty()
            .WithMessage("Site URL cannot be empty.")
            .Length(1, 250)
            .WithMessage("Site URL length must be between 1 and 250 characters.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Site URL must not start or end with whitespace.");

        RuleFor(x => x.ResourceType)
            .NotNull()
            .WithMessage("Resource Type cannot be null.")
            .IsInEnum()
            .WithMessage("Invalid Resource Type.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !String.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary length must be between 1 and 10,000 characters when Summary is not empty.");

        RuleFor(x => x.PledgeId)
            .GreaterThan(0)
            .WithMessage("PledgeId must be greater than 0.");
    }
}
