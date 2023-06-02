using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Parties;

public sealed class GetByNameQuery : IRequest<Page<PartyResponse>>
{
    public string Name { get; set; }

    public PageOptions PageOptions { get; set; }
}