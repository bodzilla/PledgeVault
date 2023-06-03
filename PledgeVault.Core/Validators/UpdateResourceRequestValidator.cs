using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using System;

namespace PledgeVault.Core.Validators;

public sealed class UpdateResourceRequestValidator : AbstractValidator<UpdateResourceRequest>
{
    public UpdateResourceRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Title.Trim())
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(1, 250)
            .WithMessage("Title length must be between 1 and 250 characters.");

        RuleFor(x => x.SiteUrl.Trim())
            .NotEmpty()
            .WithMessage("Site URL cannot be empty.")
            .Length(1, 250)
            .WithMessage("Site URL length must be between 1 and 250 characters.");

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
            .WithMessage("Pledge Id must be greater than 0.");
    }
}
