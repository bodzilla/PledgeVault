using System.Collections.Generic;
using MediatR;
using PledgeVault.Core.Dtos.Responses;
using PledgeVault.Core.Enums;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetCountriesByGovernmentTypeQuery : IRequest<IEnumerable<CountryResponse>>
{
    public GovernmentType GovernmentType { get; set; }
}