using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Politicians;

public sealed record GetByPartyIdQuery : IRequest<Page<PoliticianResponse>>
{
    public int Id { get; init; }

    public PageOptions PageOptions { get; init; }
}