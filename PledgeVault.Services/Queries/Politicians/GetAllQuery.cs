using MediatR;
using PledgeVault.Core.Dtos.Pagination;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Politicians;

public sealed class GetAllQuery : IRequest<Page<PoliticianResponse>>
{
    public PageOptions PageOptions { get; set; }
}