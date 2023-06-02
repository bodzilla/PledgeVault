using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetAllQuery<TResponse> : IRequest<PageResponse<TResponse>> where TResponse : IResponse
{
    public Page Page { get; set; }
}