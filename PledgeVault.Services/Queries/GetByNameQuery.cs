using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed record GetByNameQuery<T> : IRequest<Page<T>> where T : IResponse
{
    public string Name { get; init; }

    public PageOptions PageOptions { get; init; }
}