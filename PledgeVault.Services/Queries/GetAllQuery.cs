using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetAllQuery<T> : IRequest<PaginationResponse<T>> where T : IResponse
{
    public PaginationQuery PaginationQuery { get; set; }
}