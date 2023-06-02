using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Resources;

public sealed class SetInactiveCommand : IRequest<ResourceResponse>
{
    public int Id { get; set; }
}