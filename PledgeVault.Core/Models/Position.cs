using PledgeVault.Core.Contracts;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Position : IEntity
{
    public Position()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Politicians = new List<Politician>();
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

    public string Title { get; set; }

    public string Summary { get; set; }

    public ICollection<Politician> Politicians { get; set; }
}