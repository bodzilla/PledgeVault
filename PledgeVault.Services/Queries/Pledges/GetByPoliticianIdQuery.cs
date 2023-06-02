using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed class GetByPoliticianIdQuery : IRequest<Page<PledgeResponse>>
{
    public int Id { get; set; }

    public PageOptions PageOptions { get; set; }
}