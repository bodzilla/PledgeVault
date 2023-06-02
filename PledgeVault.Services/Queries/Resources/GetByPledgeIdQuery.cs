using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Resources;

public sealed class GetByPledgeIdQuery : IRequest<Page<ResourceResponse>>
{
    public int Id { get; set; }

    public PageOptions PageOptions { get; set; }
}