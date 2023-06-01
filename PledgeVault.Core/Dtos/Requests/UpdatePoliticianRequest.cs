using PledgeVault.Core.Enums;
using System;
using PledgeVault.Core.Contracts;

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

    public int PositionId { get; set; }

    public bool IsPartyLeader { get; set; }

    public string PhotoUrl { get; set; }

    public string Summary { get; set; }
}