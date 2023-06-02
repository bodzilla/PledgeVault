using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetAllQuery : IRequest<Page<CountryResponse>>
{
    public PageOptions PageOptions { get; set; }
}