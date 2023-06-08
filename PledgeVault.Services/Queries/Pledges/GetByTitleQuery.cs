using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed record GetByTitleQuery : IRequest<Page<PledgeResponse>>
{
    public string Title { get; init; }

    public PageOptions PageOptions { get; init; }
}