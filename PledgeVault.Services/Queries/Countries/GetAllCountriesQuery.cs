using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetAllCountriesQuery : IRequest<PaginationResponse<CountryResponse>>
{
    public PaginationQuery PaginationQuery { get; set; }
}