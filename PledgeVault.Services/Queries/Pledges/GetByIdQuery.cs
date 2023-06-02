using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Pledges;

public sealed class GetByIdQuery : IRequest<PledgeResponse>
{
    public int Id { get; set; }
}