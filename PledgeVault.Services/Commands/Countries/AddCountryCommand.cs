using MediatR;
using PledgeVault.Core.Dtos.Requests;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Countries;

public sealed class AddCountryCommand : IRequest<CountryResponse>
{
    public AddCountryRequest Request { get; set; }
}