using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Enums;
using System;

namespace PledgeVault.Core.Validators;

public sealed class UpdatePledgeRequestValidator : AbstractValidator<UpdatePledgeRequest>
{
    public UpdatePledgeRequestValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Request object cannot be null.");

        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("Id must be greater than 0.");

        RuleFor(x => x.Title)
            .NotEmpty()
            .WithMessage("Title cannot be empty.")
            .Length(1, 250)
            .WithMessage("Title length must be between 1 and 250 characters.")
            .Must(x => x is null || x.Trim() == x)
            .WithMessage("Title must not start or end with whitespace.");

        RuleFor(x => x.DatePledged)
            .NotEmpty()
            .WithMessage("Date Pledged cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now)
            .WithMessage("Date Pledged must be a date in the past.");

        RuleFor(x => x.DateFulfilled)
            .NotEmpty()
            .WithMessage("Date Fulfilled cannot be empty.")
            .LessThanOrEqualTo(DateTime.Now)
            .When(x => x.PledgeStatusType is PledgeStatusType.FulfilledExact or PledgeStatusType.FulfilledPartial)
            .WithMessage("Date Fulfilled must be a date in the past when Pledge Status Type is 'FulfilledExact' or 'FulfilledPartial'.");

        RuleFor(x => x.PledgeCategoryType)
            .NotNull()
            .WithMessage("Pledge Category Type cannot be null.")
            .IsInEnum()
            .WithMessage("Invalid Pledge Category Type.");

        RuleFor(x => x.PledgeStatusType)
            .NotNull()
            .WithMessage("Pledge Status Type cannot be null.")
            .IsInEnum()
            .WithMessage("Invalid Pledge Status Type.");

        RuleFor(x => x.PoliticianId)
            .GreaterThan(0)
            .WithMessage("PoliticianId must be greater than 0.");

        RuleFor(x => x.Summary.Trim())
            .Length(1, 10000)
            .When(x => !String.IsNullOrWhiteSpace(x.Summary))
            .WithMessage("Summary length must be between 1 and 10,000 characters when Summary is not empty.");

        RuleFor(x => x.FulfilledSummary.Trim())
            .Length(1, 10000)
            .When(x => x.PledgeStatusType is PledgeStatusType.FulfilledExact or PledgeStatusType.FulfilledPartial)
            .WithMessage("Fulfilled Summary length must be between 1 and 10,000 characters when Pledge Status Type is 'FulfilledExact' or 'FulfilledPartial'.");

        RuleFor(x => x.FulfilledSummary)
            .Empty()
            .When(x => x.PledgeStatusType is not PledgeStatusType.FulfilledExact and PledgeStatusType.FulfilledPartial)
            .WithMessage("Fulfilled Summary must be empty when Pledge Status Type is not 'FulfilledExact' and 'FulfilledPartial'.");
    }
}
