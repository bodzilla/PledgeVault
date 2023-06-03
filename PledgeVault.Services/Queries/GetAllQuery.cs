using MediatR;
using PledgeVault.Core.Contracts;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetAllQuery<TEntity, TResponse> : IRequest<Page<TResponse>>
    where TEntity : IEntity
    where TResponse : IResponse
{
    public PageOptions PageOptions { get; set; }
}