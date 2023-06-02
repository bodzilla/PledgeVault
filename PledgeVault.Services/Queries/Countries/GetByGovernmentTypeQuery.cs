using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetByGovernmentTypeQuery : IRequest<Page<CountryResponse>>
{
    public GovernmentType GovernmentType { get; set; }

    public PageOptions PageOptions { get; set; }
}