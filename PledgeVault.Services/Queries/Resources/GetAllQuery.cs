using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Resources;

public sealed class GetAllQuery : IRequest<Page<ResourceResponse>>
{
    public PageOptions PageOptions { get; set; }
}