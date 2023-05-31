using System;
using System.Collections.Generic;
using PledgeVault.Core.Base;

namespace PledgeVault.Core.Models;

public sealed class Party : EntityBase
{
    public Party() => Politicians = new List<Politician>();

    public string Name { get; set; }

    public DateTime? DateEstablished { get; set; }

    public int CountryId { get; set; }

    public Country Country { get; set; }

    public string LogoUrl { get; set; }

    public string SiteUrl { get; set; }

    public string Summary { get; set; }

    public ICollection<Politician> Politicians { get; set; }
}