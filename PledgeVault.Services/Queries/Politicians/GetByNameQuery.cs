using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Politicians;

public sealed class GetByNameQuery : IRequest<Page<PoliticianResponse>>
{
    public string Name { get; set; }

    public PageOptions PageOptions { get; set; }
}