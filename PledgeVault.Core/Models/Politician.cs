﻿using PledgeVault.Core.Contracts;
using PledgeVault.Core.Enums;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Politician : IEntity
{
    public Politician()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Pledges = new List<Pledge>();
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

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