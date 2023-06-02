using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Parties;

public sealed class UpdateCommand : IRequest<PartyResponse>
{
    public UpdatePartyRequest Request { get; set; }
}