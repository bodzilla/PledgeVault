using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Parties;

public sealed class SetInactiveCommand : IRequest<PartyResponse>
{
    public int Id { get; set; }
}