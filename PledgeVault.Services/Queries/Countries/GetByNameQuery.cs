using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetByNameQuery : IRequest<Page<CountryResponse>>
{
    public string Name { get; set; }

    public PageOptions PageOptions { get; set; }
}