using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Pledges;

public sealed class SetInactiveCommand : IRequest<PledgeResponse>
{
    public int Id { get; set; }
}