using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Pledges;

public sealed class AddCommand : IRequest<PledgeResponse>
{
    public AddPledgeRequest Request { get; set; }
}
