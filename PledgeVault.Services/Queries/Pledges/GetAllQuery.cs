using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed class GetAllQuery : IRequest<Page<PledgeResponse>>
{
    public PageOptions PageOptions { get; set; }
}