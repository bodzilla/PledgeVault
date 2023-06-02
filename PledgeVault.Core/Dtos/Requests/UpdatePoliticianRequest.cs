using PledgeVault.Core.Contracts.Dtos;
using PledgeVault.Core.Enums;
using System;

namespace PledgeVault.Core.Dtos.Requests;

public sealed class UpdatePoliticianRequest : IRequest
{
    public int Id { get; set; }

    public string Name { get; set; }

    public SexType SexType { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public string CountryOfBirth { get; set; }

    public int PartyId { get; set; }

    public string Position { get; set; }

    public bool IsPartyLeader { get; set; }

    public string PhotoUrl { get; set; }

    public string Summary { get; set; }
}