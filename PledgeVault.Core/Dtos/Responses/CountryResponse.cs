using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed class CountryResponse : IResponse
{
    public CountryResponse() => Parties = new List<PartyResponse>();

    public int Id { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }

    public ICollection<PartyResponse> Parties { get; set; }
}