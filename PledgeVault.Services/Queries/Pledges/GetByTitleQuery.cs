using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed class GetByTitleQuery : IRequest<Page<PledgeResponse>>
{
    public string Title { get; set; }

    public PageOptions PageOptions { get; set; }
}