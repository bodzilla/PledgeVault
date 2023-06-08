using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Dtos.Responses;

public sealed record PoliticianResponse : IResponse
{
    public int Id { get; init; }

    public string Name { get; init; }

    public SexType SexType { get; init; }

    public DateTime DateOfBirth { get; init; }

    public DateTime? DateOfDeath { get; init; }

    public string CountryOfBirth { get; init; }

    public PartyResponse Party { get; init; }

    public string Position { get; init; }

    public bool IsPartyLeader { get; init; }

    public string PhotoUrl { get; init; }

    public string Summary { get; init; }

    public IReadOnlyCollection<PledgeResponse> Pledges { get; init; }
}