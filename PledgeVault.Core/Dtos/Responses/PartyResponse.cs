using PledgeVault.Core.Contracts.Dtos;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record PartyResponse : IResponse
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime? DateEstablished { get; init; }

    public CountryResponse Country { get; init; }

    public string LogoUrl { get; init; }

    public string SiteUrl { get; init; }

    public string Summary { get; init; }

    public IReadOnlyCollection<PoliticianResponse> Politicians { get; init; }
}