using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Politicians;

public sealed class GetByIdQuery : IRequest<PoliticianResponse>
{
    public int Id { get; set; }
}