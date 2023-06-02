using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Parties;

public sealed class GetByIdQuery : IRequest<PartyResponse>
{
    public int Id { get; set; }
}