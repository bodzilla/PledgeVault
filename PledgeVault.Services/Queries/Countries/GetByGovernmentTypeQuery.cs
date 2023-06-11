using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums.Models;

namespace PledgeVault.Services.Queries.Countries;

public sealed record GetByGovernmentTypeQuery : IRequest<Page<CountryResponse>>
{
    public GovernmentType GovernmentType { get; init; }

    public PageOptions PageOptions { get; init; }
}