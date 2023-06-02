using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Parties;

public sealed class GetAllQuery : IRequest<Page<PartyResponse>>
{
    public PageOptions PageOptions { get; set; }
}