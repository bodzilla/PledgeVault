using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Enums.Models;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Politician : IEntity
{
    public Politician()
    {
        EntityCreated = DateTime.Now;
        IsEntityActive = true;
        Pledges = new List<Pledge>();
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public string Name { get; set; }

    public SexType SexType { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime? DateOfDeath { get; set; }

    public string CountryOfBirth { get; set; }

    public int PartyId { get; init; }

    public Party Party { get; init; }

    public string Position { get; set; }

    public bool IsPartyLeader { get; set; }

    public string PhotoUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<Pledge> Pledges { get; init; }
}