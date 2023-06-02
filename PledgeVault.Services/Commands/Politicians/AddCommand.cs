using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Politicians;

public sealed class AddCommand : IRequest<PoliticianResponse>
{
    public AddPoliticianRequest Request { get; set; }
}
