﻿using PledgeVault.Core.Dtos.Requests;

namespace PledgeVault.Core.Models.Validators;

using FluentValidation;
using System;

public sealed class AddPledgeRequestValidator : AbstractValidator<AddPledgeRequest>
{
    public AddPledgeRequestValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x.Title.Trim()).NotEmpty().Length(1, 250);
        RuleFor(x => x.DatePledged).NotEmpty().LessThanOrEqualTo(DateTime.Now);
        RuleFor(x => x.DateFulfilled).NotEmpty().LessThanOrEqualTo(DateTime.Now).When(x => x.DateFulfilled.HasValue);
        RuleFor(x => x.PledgeCategoryType).NotNull().IsInEnum();
        RuleFor(x => x.PledgeStatusType).NotNull().IsInEnum();
        RuleFor(x => x.PoliticianId).GreaterThan(0);
        RuleFor(x => x.Summary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.Summary));

        RuleFor(x => x.FulfilledSummary.Trim()).Length(1, 10000).When(x => !String.IsNullOrWhiteSpace(x.FulfilledSummary));
        RuleFor(x => x.FulfilledSummary).Empty().When(x => !x.DateFulfilled.HasValue);
    }
}
