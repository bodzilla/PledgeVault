using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Politicians;

public sealed class SetInactiveCommand : IRequest<PoliticianResponse>
{
    public int Id { get; set; }
}