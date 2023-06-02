using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Resources;

public sealed class UpdateCommand : IRequest<ResourceResponse>
{
    public UpdateResourceRequest Request { get; set; }
}