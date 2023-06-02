﻿using System.Collections.Generic;
using MediatR;
using PledgeVault.Core.Dtos.Responses;

namespace PledgeVault.Services.Queries.Countries;

public sealed class GetCountriesByNameQuery : IRequest<IEnumerable<CountryResponse>>
{
    public string Name { get; set; }
}