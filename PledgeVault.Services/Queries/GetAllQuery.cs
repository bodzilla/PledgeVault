using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetAllQuery<T> : IRequest<Page<T>> where T : IResponse
{
    public PageOptions PageOptions { get; set; }
}