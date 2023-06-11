using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record CountryResponse : IResponse
{
    public int Id { get; init; }

    public string Name { get; init; }

    public DateTime? DateEstablished { get; init; }

    public GovernmentType GovernmentType { get; init; }

    public string Summary { get; init; }

    public IReadOnlyCollection<PartyResponse> Parties { get; init; }
}