using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetByGovernmentTypeQuery : IRequest<PageResponse<CountryResponse>>
{
    public GovernmentType GovernmentType { get; set; }

    public Page Page { get; set; }
}