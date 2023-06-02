using MediatR;
using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Dtos.Pagination;

namespace PledgeVault.Services.Queries;

public sealed class GetByNameQuery<T> : IRequest<PaginationResponse<T>> where T : IResponse
{
    public string Name { get; set; }

    public PaginationQuery PaginationQuery { get; set; }
}