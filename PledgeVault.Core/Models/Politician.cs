using System.Collections.Generic;
using System;
using PledgeVault.Core.Base;
using PledgeVault.Core.Enums;

namespace PledgeVault.Core.Models;

public sealed class Politician : EntityBase
{
    public Politician()
    {
        IsPartyLeader = false;
        Pledges = new List<Pledge>();
    }

    public string Name { get; set; }

    public SexType SexType { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public string CountryOfBirth { get; set; }

    public int PartyId { get; set; }

    public Party Party { get; set; }

    public int PositionId { get; set; }

    public Position Position { get; set; }

    public bool IsPartyLeader { get; set; }

    public string PhotoUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<Pledge> Pledges { get; set; }
}