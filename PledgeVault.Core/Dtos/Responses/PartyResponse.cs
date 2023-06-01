using System.Collections.Generic;
using System;
using PledgeVault.Core.Contracts;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class PartyResponse : IResponse
{
    public PartyResponse() => Politicians = new List<PoliticianResponse>();

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public CountryResponse Country { get; set; }

    public string LogoUrl { get; set; }

    public string SiteUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<PoliticianResponse> Politicians { get; set; }
}