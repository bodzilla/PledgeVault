using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Parties;

public sealed record GetByCountryIdQuery : IRequest<Page<PartyResponse>>
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}