using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed record GetByUserIdQuery<T> : IRequest<Page<T>> where T : IResponse
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}