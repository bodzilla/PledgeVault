using PledgeVault.Core.Contracts.Entities;
using System;
using System.Collections.Generic;

namespace PledgeVault.Core.Models;

public sealed class Party : IEntity
{
    public Party()
    {
        EntityCreated = DateTime.Now;
        IsEntityActive = true;
        Politicians = new List<Politician>();
    }

    public int Id { get; init; }

    public DateTime EntityCreated { get; init; }

    public DateTime? EntityModified { get; set; }

    public bool IsEntityActive { get; set; }

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public int CountryId { get; init; }

    public Country Country { get; init; }

    public string LogoUrl { get; set; }

    public string SiteUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<Politician> Politicians { get; init; }
}