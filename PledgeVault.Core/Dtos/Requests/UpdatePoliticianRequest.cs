using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums.Models;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed record UpdatePoliticianRequest : IRequest
{
    public int Id { get; init; }

    public string Name { get; init; }

    public SexType SexType { get; init; }

    public DateTime DateOfBirth { get; init; }

    public DateTime? DateOfDeath { get; init; }

    public string CountryOfBirth { get; init; }

    public int PartyId { get; init; }

    public string Position { get; init; }

    public bool IsPartyLeader { get; init; }

    public string PhotoUrl { get; init; }

    public string Summary { get; init; }
}