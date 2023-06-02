using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Politicians;

public sealed class GetByPartyIdQuery : IRequest<Page<PoliticianResponse>>
{
    public int Id { get; set; }

    public PageOptions PageOptions { get; set; }
}