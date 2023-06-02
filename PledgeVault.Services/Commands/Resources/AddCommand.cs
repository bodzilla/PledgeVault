using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Resources;

public sealed class AddCommand : IRequest<ResourceResponse>
{
    public AddResourceRequest Request { get; set; }
}
