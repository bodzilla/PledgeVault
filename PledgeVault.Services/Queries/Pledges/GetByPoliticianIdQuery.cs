using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed record GetByPoliticianIdQuery : IRequest<Page<PledgeResponse>>
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}