using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetCountriesByNameQuery : IRequest<PaginationResponse<CountryResponse>>
{
    public string Name { get; set; }

    public PaginationQuery PaginationQuery { get; set; }
}