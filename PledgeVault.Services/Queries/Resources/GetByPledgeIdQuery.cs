using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Resources;

public sealed record GetByPledgeIdQuery : IRequest<Page<ResourceResponse>>
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}