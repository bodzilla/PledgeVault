using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Pledges;

public sealed class UpdateCommand : IRequest<PledgeResponse>
{
    public UpdatePledgeRequest Request { get; set; }
}