using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetAllQuery<TResponse> : IRequest<PaginationResponse<TResponse>> where TResponse : IResponse
{
    public PaginationQuery PaginationQuery { get; set; }
}