using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Politicians;

public sealed class UpdateCommand : IRequest<PoliticianResponse>
{
    public UpdatePoliticianRequest Request { get; set; }
}