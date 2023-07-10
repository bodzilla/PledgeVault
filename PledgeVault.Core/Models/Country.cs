using PledgeVault.Core.Contracts.Entities;
using PledgeVault.Core.Enums.Models;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Country : IEntity
{
    public Country()
    {
        EntityCreated = DateTime.Now;
        IsEntityActive = true;
        Parties = new List<Party>();
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }

    public ICollection<Party> Parties { get; init; }
}