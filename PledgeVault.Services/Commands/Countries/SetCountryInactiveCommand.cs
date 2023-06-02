using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Commands.Countries;

public sealed class SetCountryInactiveCommand : IRequest<CountryResponse>
{
    public int Id { get; set; }
}