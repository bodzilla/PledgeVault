using FluentValidation;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Core.Validators;

public sealed class PageOptionsValidator : AbstractValidator<PageOptions>
{
    public PageOptionsValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .WithMessage("Page Options cannot be null.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Page number must be greater than or equal to 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(25)
            .WithMessage("Page size must be greater than or equal to 25.")
            .LessThanOrEqualTo(50)
            .WithMessage("Page size must be less than or equal to 50.");
    }
}