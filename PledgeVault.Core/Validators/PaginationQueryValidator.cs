namespace PledgeVault.Core.Validators;

using FluentValidation;
using PledgeVault.Core.Dtos.Pagination;

public sealed class PaginationQueryValidator : AbstractValidator<PageOptions>
{
    public PaginationQueryValidator()
    {
        RuleFor(x => x).NotNull();

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1).WithMessage("Page number must be at least 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(25).WithMessage("Page size must be at least 25.")
            .LessThanOrEqualTo(50).WithMessage("Page size must not exceed 50.");
    }
}