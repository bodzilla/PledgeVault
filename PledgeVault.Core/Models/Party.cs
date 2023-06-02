using PledgeVault.Core.Contracts;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Party : IEntity
{
    public Party()
    {
        EntityCreated = DateTime.Now;
        EntityActive = true;
        Politicians = new List<Politician>();
    }

    public int Id { get; set; }

    public DateTime EntityCreated { get; set; }

    public DateTime? EntityModified { get; set; }

    public bool EntityActive { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public int CountryId { get; set; }

    public Country Country { get; set; }

    public string LogoUrl { get; set; }

    public string SiteUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<Politician> Politicians { get; set; }
}