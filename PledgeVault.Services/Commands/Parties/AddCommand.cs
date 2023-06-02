using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Parties;

public sealed class AddCommand : IRequest<PartyResponse>
{
    public AddPartyRequest Request { get; set; }
}
