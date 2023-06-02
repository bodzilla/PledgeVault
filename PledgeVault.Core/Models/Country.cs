using PledgeVault.Core.Contracts;
using PledgeVault.Core.Enums;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Country : IEntity
{
    public Country()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Parties = new List<Party>();
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public GovernmentType GovernmentType { get; set; }

    public string Summary { get; set; }

    public ICollection<Party> Parties { get; set; }
}