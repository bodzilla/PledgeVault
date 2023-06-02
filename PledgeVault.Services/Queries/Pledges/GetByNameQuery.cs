using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed class GetByNameQuery : IRequest<Page<PledgeResponse>>
{
    public string Name { get; set; }

    public PageOptions PageOptions { get; set; }
}