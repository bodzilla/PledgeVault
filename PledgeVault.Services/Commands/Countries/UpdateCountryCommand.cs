using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Countries;

public sealed class UpdateCountryCommand : IRequest<CountryResponse>
{
    public UpdateCountryRequest Request { get; set; }
}