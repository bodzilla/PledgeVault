using FluentValidation;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Enums;
using System;

namespace PledgeVault.Core.Validators;

public sealed class AddPledgeRequestValidator : AbstractValidator<AddPledgeRequest>
{
    public AddPledgeRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Title.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.DatePledged).NotEmpty().LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.DateFulfilled).NotEmpty().LessThanOrEqualTo(DateTime.Now).When(x => x.PledgeStatusType is PledgeStatusType.FulfilledExact or PledgeStatusType.FulfilledPartial);
        RuleFor(x => x.PledgeCategoryType).NotNull().IsInEnum();
        RuleFor(x => x.PledgeStatusType).NotNull().IsInEnum();
        RuleFor(x => x.PoliticianId).GreaterThan(0);
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.FulfilledSummary.Trim()).Length(1, 10000).When(x => x.PledgeStatusType is PledgeStatusType.FulfilledExact or PledgeStatusType.FulfilledPartial);
        RuleFor(x => x.FulfilledSummary).Empty().When(x => x.PledgeStatusType is not PledgeStatusType.FulfilledExact and PledgeStatusType.FulfilledPartial);
    }
}
